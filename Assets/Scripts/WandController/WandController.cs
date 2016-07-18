using UnityEngine;
using System.Collections;

public class WandController : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown { get { return controller.GetPressDown(gripButton); } }
    public bool gripButtonUp { get { return controller.GetPressUp(gripButton); } }
    public bool gripButtonPressed { get { return controller.GetPress(gripButton); } }

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool TriggerButtonDown { get { return controller.GetPressDown(triggerButton); } }
    public bool triggerButtonUp { get { return controller.GetPressUp(triggerButton); } }
    public bool triggerButtonPressed { get { return controller.GetPress(triggerButton); } }

    private Valve.VR.EVRButtonId touchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    public bool touchpadButtonDown { get { return controller.GetPressDown(touchpadButton); } }
    public bool touchpadButtonUp { get { return controller.GetPressUp(touchpadButton); } }
    public bool touchpadButtonPressed { get { return controller.GetPress(touchpadButton); } }

    private Valve.VR.EVRButtonId applicationMenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    public bool applicationMenuButtonDown { get { return controller.GetPressDown(applicationMenuButton); } }
    public bool applicationMenuButtonUp { get { return controller.GetPressUp(applicationMenuButton); } }
    public bool applicationMenuButtonPressed { get { return controller.GetPress(applicationMenuButton); } }

    private Valve.VR.EVRButtonId dpadDownButton = Valve.VR.EVRButtonId.k_EButton_DPad_Down;
    public bool dpadDownButtonDown { get { return controller.GetPressDown(dpadDownButton); } }
    public bool dpadDownButtonUp { get { return controller.GetPressUp(dpadDownButton); } }
    public bool dpadDownButtonPressed { get { return controller.GetPress(dpadDownButton); } }



    /*
    * Axis 0 controls location of touchpad
    */
    private Valve.VR.EVRButtonId Axis0Id = Valve.VR.EVRButtonId.k_EButton_Axis0;
    public Vector2 Axis0 { get { return controller.GetAxis(Axis0Id); } }

    /*
    * Axis 1 controls trigger
    */ 
    private Valve.VR.EVRButtonId Axis1Id = Valve.VR.EVRButtonId.k_EButton_Axis1;
    public Vector2 Axis1 { get { return controller.GetAxis(Axis1Id); } }

    private Valve.VR.EVRButtonId Axis2Id = Valve.VR.EVRButtonId.k_EButton_Axis2;
    public Vector2 Axis2 { get { return controller.GetAxis(Axis2Id); } }

    private Valve.VR.EVRButtonId Axis3Id = Valve.VR.EVRButtonId.k_EButton_Axis3;
    public Vector2 Axis3 { get { return controller.GetAxis(Axis3Id); } }

    private Valve.VR.EVRButtonId Axis4Id = Valve.VR.EVRButtonId.k_EButton_Axis4;
    public Vector2 Axis4 { get { return controller.GetAxis(Axis4Id); } }


    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
    // Use this for initialization
    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update() {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }
        //Debug.Log("ApplicationMenu " + applicationMenuButtonDown);
        //Debug.Log("Dpad Down: " + dpadDownButtonDown);
        //Debug.Log("Axis 0: " + Axis0);
        //Debug.Log("Axis 1: " + Axis1);
        //Debug.Log("Axis 2: " + Axis2);
        //Debug.Log("Axis 3: " + Axis3);
        //Debug.Log("Axis 4: " + Axis4);

    }

    public void Vibrate(float length)
    {
        StartCoroutine(LongVibration(length, 1));
    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
        yield return null;
        }
    }
}
