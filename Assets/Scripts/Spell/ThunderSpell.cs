using UnityEngine;
using System.Collections;
using VRTK;

public class ThunderSpell : BaseSpell {

    private GameObject thunderParticles;
    private float lifetime = 3;
    private float yOffset = 0f;

    public override void Awake()
    {
        base.Awake();
        thunderParticles = Resources.Load("SpellEffects/Thunder") as GameObject;
        InitPointer();
    }

    public override void CastSpell(object sender, ControllerInteractionEventArgs e)
    {
        base.CastSpell(sender, e);
        GameObject boom = SpawnObjectAtPointer(thunderParticles);
        if (boom != null)
        {
            boom.transform.position = new Vector3(boom.transform.position.x, yOffset, boom.transform.position.z);
            boom.transform.rotation = thunderParticles.transform.rotation;
            Destroy(boom.GetComponent<Collider>(), .1f);
            Destroy(boom, lifetime); boom.transform.position = new Vector3(boom.transform.position.x, yOffset, boom.transform.position.z);
            boom.transform.rotation = thunderParticles.transform.rotation;
            Destroy(boom.GetComponent<Collider>(), .1f);
            Destroy(boom, lifetime);
        }
       
    }
}
