using UnityEngine;
using System.Collections;
using VRTK;
using System.Collections.Generic;

public class Wand : VRTK_InteractableObject {

    private bool canCreateSpellWheel = true;

    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);
        gameObject.AddComponent<MagicCircle>();

        //StartCoroutine(WaitTilComponentExists());
    }

    public override void StartUsing(GameObject currentUsingObject)
    {
        base.StartUsing(currentUsingObject);

        Destroy(GetComponent<BaseSpell>());
        GetComponent<CircleSelector>().CreateCircle(Resources.Load("Weapons/orb") as GameObject, 4, .2f, .2f,1);
        
    }

    public void DisableUsing()
    {
        canCreateSpellWheel = false;

    }

    //public void DestinationSetTest(object sender, DestinationMarkerEventArgs e)
    //{
    //    Debug.Log("Distance " + e.distance);
    //    Debug.Log("Location " + e.target);
    //}

    //IEnumerator WaitTilComponentExists()
    //{
    //    while (GetComponentInParent<VRTK_SimplePointer>() == null)
    //    {
    //        Debug.Log("Null");
    //        yield return null;
    //    }
    //    GetComponentInParent<VRTK_SimplePointer>().DestinationMarkerSet += new DestinationMarkerEventHandler(DestinationSetTest);
    //    Debug.Log("Found");
    //}



  




    //protected override void Start()
    //{
    //    base.Start();
    //    GetComponentInParent<VRTK_ControllerEvents>().useToggleButton = VRTK_ControllerEvents.ButtonAlias.Touchpad_Press;
    //}

    //protected override void OnDisable()
    //{
    //    base.OnDisable();
    //    GetComponentInParent<VRTK_ControllerEvents>().useToggleButton = VRTK_ControllerEvents.ButtonAlias.Trigger;
    //}



    //IEnumerator RotateParent (GameObject parent, float speed)
    //{
    //    var ang = 0f;
    //    while (ang < 360)
    //    {
    //        Debug.Log(ang);
    //        ang += Time.deltaTime * speed;
    //        parent.transform.rotation = Quaternion.Euler(0, 0, ang);
    //        yield return null;
    //    }
    //}

}
