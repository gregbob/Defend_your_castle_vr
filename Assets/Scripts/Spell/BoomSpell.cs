using UnityEngine;
using System.Collections;
using VRTK;

public class BoomSpell : BaseSpell {

    private GameObject boomParticles;
    private float lifetime = 7.5f;
    private float yOffset = .5f;

    public override void Awake ()
    {
        base.Awake();
        boomParticles = Resources.Load("SpellEffects/Boom") as GameObject;
        InitPointer();
    }

    public override void CastSpell(object sender, ControllerInteractionEventArgs e)
    {
        base.CastSpell(sender, e);
        GameObject boom = SpawnObjectAtPointer(boomParticles);
        if (boom != null)
        {
            boom.transform.position = new Vector3(boom.transform.position.x, yOffset, boom.transform.position.z);
            Destroy(boom.GetComponent<Collider>(), .5f);
            Destroy(boom, lifetime);
        }
        
    }
}
