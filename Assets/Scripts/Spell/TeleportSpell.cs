using UnityEngine;
using System.Collections;
using VRTK;
public class TeleportSpell : BaseSpell {

	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Awake()
    {
        base.Awake();
        GetComponentInParent<VRTK_BasicTeleport>().Teleported += new TeleportEventHandler(OnTeleport);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        GetComponentInParent<VRTK_BasicTeleport>().Teleported -= new TeleportEventHandler(OnTeleport);
        //GetComponentInParent<VRTK_ControllerEvents>().pointerSetButton = VRTK_ControllerEvents.ButtonAlias.Touchpad_Press;
       // GetComponentInParent<VRTK_ControllerEvents>().pointerToggleButton = VRTK_ControllerEvents.ButtonAlias.Touchpad_Press;
    }

    public override void CastSpell(object sender, ControllerInteractionEventArgs e)
    {
        base.CastSpell(sender, e);
        Debug.Log("Hi from teleport");
       // TurnPointerOff();
        //Destroy(this);

    }

    public void OnTeleport(object sender, DestinationMarkerEventArgs e)
    {
        GetComponentInParent<VRTK_BezierPointer>().enableTeleport = false;
    }

    public void TurnPointerOff()
    {
        Debug.Log("Turning pointer off");
        var pointer = GetComponentInParent<VRTK_BezierPointer>();
        pointer.enableTeleport = false;
        pointer.pointerVisibility = VRTK_WorldPointer.pointerVisibilityStates.Always_Off;
        pointer.showPlayAreaCursor = false;
        pointer.showPointerCursor = false;
       
    }

    public void TurnPointerOn()
    {
        var pointer = GetComponentInParent<VRTK_BezierPointer>();
        pointer.enableTeleport = true;
        pointer.pointerVisibility = VRTK_WorldPointer.pointerVisibilityStates.Always_On;
        pointer.showPlayAreaCursor = true;
        pointer.showPointerCursor = true;
    }
}
