using UnityEngine;
using System.Collections;

public class CircleNode : MonoBehaviour {

    public float speed = 10;
    public CircleSelector selector;
    public int index;



    [HideInInspector]
   // public Vector3 target;

    private bool triggerable = false;


	
	// Update is called once per frame
	void Update () {
        transform.Rotate(30 * Time.deltaTime, 30 * Time.deltaTime, 30 * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CircleNode>() == null && triggerable && selector.CanSelect(this))
        {
            GetComponent<Renderer>().material.color = Utility.GetRandomColor();
            StartCoroutine(GrowAndShrink(8));
            selector.AddSelectedNode(this);
        }
            

    }


    public void MoveTowards(Vector3 targetPos)
    {
        StartCoroutine(MoveTowardsAux(targetPos));
    }


    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return base.Equals(obj) && index == ((CircleNode)obj).index;
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return base.GetHashCode() ^ index * 13;
    }

    IEnumerator MoveTowardsAux(Vector3 targetPos)
    {
        while (!Utility.Approximately(transform.position, targetPos, .01f))
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
            yield return null;
        }
        triggerable = true;
    }

    IEnumerator GrowAndShrink(float growSpeed)
    {
        AnimationCurve growShrink = CreateGrowShrinkAnimationCurve();

        var time = 0f;
        var x = transform.localScale.x;
        var y = transform.localScale.y;
        var z = transform.localScale.z;

        while (time <= 1)
        {
            time += Time.deltaTime * growSpeed;
            transform.localScale = new Vector3(x * growShrink.Evaluate(time), y * growShrink.Evaluate(time), z * growShrink.Evaluate(time));
            yield return null;
        }

    }



    private AnimationCurve CreateGrowShrinkAnimationCurve()
    {
        AnimationCurve growShrink = new AnimationCurve();
        growShrink.AddKey(0, 1);
        growShrink.AddKey(.5f, 1.25f);
        //growShrink.AddKey(.75f, .75f);
        growShrink.AddKey(1, 1);

        return growShrink;
    }


}
