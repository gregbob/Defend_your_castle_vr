using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchpadSelector : MonoBehaviour {

    private WandController controller;
    
    private bool menuOpen;
    private int size = 45;
    public float deadzone;

    [Header("GUI")]
    public Canvas selector;
    public Image dot;

    private Teleport teleport;

    
    // Use this for initialization
    void Awake () {
        controller = GetComponent<WandController>();
        teleport = GetComponent<Teleport>();
        selector.enabled = false;
        deadzone = .1f;
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (controller.touchpadButtonDown)
        {
            Debug.Log("down");
            selector.enabled = true;
        }
        if (controller.touchpadButtonPressed)
        {
            //dot.rectTransform.position = new Vector3(controller.Axis0.x * size, controller.Axis0.y * size, 0);
            dot.transform.localPosition = new Vector3(controller.Axis0.x * size * -1, controller.Axis0.y * size * -1, 0);
        }
        if (controller.touchpadButtonUp)
        {
            selector.enabled = false;
            teleport.TeleportTo(GetQuadrant());
        }

    }

    public int GetQuadrant()
    {
        Vector2 axis = controller.Axis0;
        // NW
        if (axis.x <= deadzone && axis.y >= deadzone)
        {
            return 0;

        }
        //NE
        else if (axis.x >= deadzone && axis.y >= deadzone)
        {
            return 1;
        }
        //SE
        else if (axis.x >= deadzone && axis.y <= deadzone)
        {
            return 2;
        }
        //SW
        else if (axis.x <= deadzone && axis.y <= deadzone)
        {
            return 3;
        }
        return 0;
    }
}
