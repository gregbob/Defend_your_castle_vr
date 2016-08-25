using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SelectionSequence {

    public int[] sequence;
    public string name;

    public SelectionSequence(int[] sequence, string name)
    {
        this.sequence = sequence;
        this.name = name;
    }

    
}

public class CircleSelector : MonoBehaviour {

    protected GameObject circle;
    protected List<CircleNode> nodes;
    protected List<CircleNode> selectedNodes;

     public delegate void Selection();
    public event Selection OnSelection;

    // Use this for initialization
    protected virtual void Awake () {
        selectedNodes = new List<CircleNode>();

       
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	}

    protected virtual void Start()
    {

    }

    public bool CheckSelectionSequence(SelectionSequence sequence)
    {
        if (sequence.sequence.Length != selectedNodes.Count)
        {
            //PrintSelected();
            //Debug.Log("Size of selected: " + selectedNodes.Count + " Size of sequence: " + sequence.sequence.Length);
            return false;
        }
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            if (selectedNodes[i].index != sequence.sequence[i])
            {
                //Debug.Log("i: " + i + " Selected index " + selectedNodes[i].index + ", sequence index :" + sequence.sequence[i]);
                return false;
            }
        }
        //Debug.Log("Selected " + sequence.name + "!");
        return true;
    }

    virtual public void AddSelectedNode(CircleNode node)
    {
        selectedNodes.Add(node);
        DrawLine(node);

    }

    public virtual void CenterNodes()
    {
        foreach (CircleNode n in nodes)
        {
            n.MoveTowards(circle.transform.position);

        }
    }

    // If node is not currently selected 
    public bool CanSelect(CircleNode node)
    {
        if (selectedNodes == null)
            return true;

        foreach(CircleNode n in selectedNodes)
        {
            if (n.Equals(node)) {
                return false;
            }
        }
        return true;
    }

    // Destroy gameobjects and clear lists
    public virtual void DestroyCircle()
    {
        Destroy(circle);

        if (nodes != null)
            nodes.Clear();
        if (selectedNodes != null)
            selectedNodes.Clear();
        
    }

    public virtual void DestroyCircle(float life)
    {
        Destroy(circle, life);

        if (nodes != null)
            nodes.Clear();
        if (selectedNodes != null)
            selectedNodes.Clear();

    }

    public virtual void CreateCircle(GameObject obj, int numEle, float radius, float distance, float size)
    {
        Vector3 center = transform.position + (transform.forward * distance);
        nodes = new List<CircleNode>();
        selectedNodes = new List<CircleNode>();
        GameObject node;

        // Check if parent exists, if it does create a new one
        if (circle != null)
        {
            Destroy(circle);
        }
        circle = new GameObject();
        circle.name = "Circle";
        circle.transform.position = center;

        // Calculate size of angle between each element in the circle
        // Make negative to make the indexes increase in CW order
        var angDelta = -360 / numEle;

        for (int i = 0; i < numEle; i++)
        {
            if (obj == null)
            {
                node = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            } else
            {
                node = Instantiate(obj);
            }

            // Create a parent for the spawned objects. Place all objects in a circle around the parent
            // so I can use the x and y axes only with z being 0. THen rotate the parent object to
            // look at the calling object
            node.transform.SetParent(circle.transform);

            var angle = angDelta * i;

            var x = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            var y = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            var z = 0;


            node.transform.localPosition = new Vector3(x, y, z);
            node.transform.localScale = new Vector3(size, size, size);
            node.GetComponent<Collider>().isTrigger = true;
            // Create reference to this CircleSelector to access fields of the parent
            CircleNode circleNode = node.AddComponent<CircleNode>();
            circleNode.selector = this;
            circleNode.index = i;
            // Add to list of all spheres
            nodes.Add(circleNode);

        }
        circle.transform.LookAt(transform);

        // Loop through spheres and set target transform to lerp towards
        // Set position to 0 so they begin lerping towards targey=t
        foreach (CircleNode cn in nodes)
        {
            var pos = cn.transform.position;
            cn.transform.position = center;
            cn.MoveTowards(pos);
        }
    }

    private void DrawLine(CircleNode n)
    {
        //if (selectedNodes.Count <= 1)
        //{
        //    Debug.Log("Only 1")
        //    return;
        //}
            

        Vector3[] points = new Vector3[selectedNodes.Count];
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            points[i] = selectedNodes[i].transform.position;
        }
        DrawLineBetweenPoints(circle, points, .02f, Color.cyan);
    }

    private void DrawLineBetweenPoints(GameObject obj, Vector3[] points, float width, Color color)
    {
        LineRenderer line = obj.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = obj.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        }
            

        line.SetVertexCount(points.Length);
        line.SetPositions(points);
        line.SetWidth(width, width);
        line.SetColors(color, color);
    }

    private void PrintSelected()
    {
        string temp = "";
        foreach(CircleNode node in selectedNodes)
        {
            temp += node.index + " ";
        }
        Debug.Log(temp);
    }

    
}
