using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public WandController leftController;
    public WandController rightController;

    public delegate void LeftTriggerDown();
    public event LeftTriggerDown OnLeftTriggerDown;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (leftController == null || rightController == null) {
			return;
		}
//	    if (leftController.TriggerButtonDown)
//        {
//           
//            if (OnLeftTriggerDown != null)
//            {
//                OnLeftTriggerDown();
//            }
//                
//        }
	}
}
