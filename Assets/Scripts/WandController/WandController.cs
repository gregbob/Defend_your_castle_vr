using UnityEngine;
using System.Collections;

public class WandController : MonoBehaviour {


    #region Fields
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

    /*
    * Seemingly unused?
    */
    private Valve.VR.EVRButtonId Axis2Id = Valve.VR.EVRButtonId.k_EButton_Axis2;
    public Vector2 Axis2 { get { return controller.GetAxis(Axis2Id); } }

    private Valve.VR.EVRButtonId Axis3Id = Valve.VR.EVRButtonId.k_EButton_Axis3;
    public Vector2 Axis3 { get { return controller.GetAxis(Axis3Id); } }

    private Valve.VR.EVRButtonId Axis4Id = Valve.VR.EVRButtonId.k_EButton_Axis4;
    public Vector2 Axis4 { get { return controller.GetAxis(Axis4Id); } }


    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
    #endregion
    #region Delegates and events
    public delegate void TriggerDown();
    public event TriggerDown OnTriggerDown;

    public delegate void TouchPadDown();
    public event TouchPadDown OnTouchPadDown;

    public delegate void ApplicationMenuDown();
    public event ApplicationMenuDown OnApplicationMenuDown;

    public delegate void GripDown();
    public event GripDown OnGripDown;

    public delegate void TriggerUp();
    public event TriggerUp OnTriggerUp;

    public delegate void TouchPadUp();
    public event TouchPadUp OnTouchPadUp;

    public delegate void ApplicationMenuUp();
    public event ApplicationMenuUp OnApplicationMenuUp;

    public delegate void GripUp();
    public event GripUp OnGripUp;

    public delegate void TriggerPress();
    public event TriggerPress OnTriggerPress;

    public delegate void TouchPadPress();
    public event TouchPadPress OnTouchPadPress;

    public delegate void ApplicationMenuPress();
    public event ApplicationMenuPress OnApplicationMenuPress;

    public delegate void GripPress();
    public event GripPress OnGripPress;
    #endregion

   
    // Use this for initialization
    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update() {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        if (TriggerButtonDown && OnTriggerDown != null)
            OnTriggerDown();

        if (triggerButtonUp && OnTriggerUp != null)
            OnTriggerUp();

        if (triggerButtonPressed && OnTriggerPress != null)
            OnTriggerPress();

        if (gripButtonDown && OnGripDown != null)
            OnGripDown();

        if (gripButtonUp && OnGripUp != null)
            OnGripUp();

        if (gripButtonPressed && OnGripPress != null)
            OnGripPress();

        if (touchpadButtonDown && OnTouchPadDown != null)
            OnTouchPadDown();

        if (touchpadButtonUp && OnTouchPadUp != null)
            OnTouchPadUp();

        if (touchpadButtonPressed && OnTouchPadPress != null)
            OnTouchPadPress();

        if (applicationMenuButtonDown && OnApplicationMenuDown != null)
            OnApplicationMenuDown();

        if (applicationMenuButtonUp && OnApplicationMenuUp != null)
            OnApplicationMenuUp();

        if (applicationMenuButtonPressed && OnApplicationMenuPress != null)
            OnApplicationMenuPress();
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
