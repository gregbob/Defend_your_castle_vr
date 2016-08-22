using UnityEngine;
using System.Collections;
using VRTK;

public class Sword : VRTK_InteractableObject
{
    private VRTK_ControllerActions controllerActions;
    private VRTK_ControllerEvents controllerEvents;
    private float impactMagnifier = 120f;
    private float collisionForce = 0f;

    public float CollisionForce()
    {
        return collisionForce;
    }

    public override void Grabbed(GameObject grabbingObject)
    {
        base.Grabbed(grabbingObject);
        controllerActions = grabbingObject.GetComponent<VRTK_ControllerActions>();
        controllerEvents = grabbingObject.GetComponent<VRTK_ControllerEvents>();
    }

    protected override void Awake()
    {
        base.Awake();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (controllerActions && controllerEvents && IsGrabbed())
        {
            collisionForce = controllerEvents.GetVelocity().magnitude * impactMagnifier;
            controllerActions.TriggerHapticPulse((ushort)collisionForce, 0.5f, 0.01f);
            if (collision.gameObject.GetComponent<Damagable>() != null)
            {
                Debug.Log("Hit " + collision.gameObject.name);
                collision.gameObject.GetComponent<Damagable>().TakeDamage(10);
            }
        }
        else
        {
            collisionForce = collision.relativeVelocity.magnitude * impactMagnifier;
        }
    }
}