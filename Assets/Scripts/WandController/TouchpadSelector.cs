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


    
    // Use this for initialization
    void Awake () {
        controller = GetComponent<WandController>();
        selector.enabled = false;
        deadzone = .1f;
        
	}

    void OnEnable ()
    {
        controller.OnTouchPadDown += OpenSelector;
        controller.OnTouchPadPress += MoveSelector;
        controller.OnTouchPadUp += CloseSelector;
    }

    void OnDisable()
    {
        controller.OnTouchPadDown -= OpenSelector;
        controller.OnTouchPadPress -= MoveSelector;
        controller.OnTouchPadUp -= CloseSelector;
    }

    void OpenSelector()
    {
        Debug.Log("Opening selector");
        selector.enabled = true;
    }

    void MoveSelector()
    {
        dot.transform.localPosition = new Vector3(controller.Axis0.x * size * -1, controller.Axis0.y * size * -1, 0);
    }

    void CloseSelector()
    {
        Debug.Log("Closing selector");
        selector.enabled = false;
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
