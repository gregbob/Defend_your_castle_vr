using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TouchpadSelector))]
public class Teleport : MonoBehaviour {

    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private Transform Steam_vr;

    private WandController controller;
    private TouchpadSelector selector;

    int pos = 0; 

	// Use this for initialization
	void Awake () {
        controller = GetComponent<WandController>();
        selector = GetComponent<TouchpadSelector>();
	}

    void OnEnable()
    {
        controller.OnTouchPadUp += TeleportTo;
    }

    public void TeleportTo()
    {
        var loc = selector.GetQuadrant();
        if (loc < waypoints.Length)
        {
            
            Steam_vr.transform.position = waypoints[loc].position;

        }
        


    }

}
