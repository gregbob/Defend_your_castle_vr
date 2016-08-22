using UnityEngine;
using System.Collections;
using VRTK;

public class FireballSpell : BaseSpell {

    private GameObject fireBallSphere;
    private GameObject fireBallProjectile;
    private float fireBallLife = 5f;


    public override void Awake()
    {
        base.Awake();
        fireBallProjectile = Resources.Load("SpellEffects/FireballProjectile") as GameObject;
        fireBallSphere = Resources.Load("SpellEffects/FireballSphere") as GameObject;
    }
    /*TODO
     Can probably turn the projectile launching into a base class method to be extended
    */

    public override void CastSpell(object sender, ControllerInteractionEventArgs e) {
        //spawn just one on awake, turn off, play from beginning when shooting. Nope, then you cant shoot multiple fireballs
        GameObject particles = Instantiate(fireBallProjectile, firingLocation.position, firingLocation.rotation) as GameObject;
        Destroy(particles, fireBallLife);

        base.CastSpell(sender, e);
        //GameObject fireBall = LaunchProjectile(fireBallProjectile, transform.forward * 1000, fireBallLife, 5);



        // Instantiate projectile at preset firinglocation
        GameObject projectile = Instantiate(fireBallSphere, firingLocation.position, firingLocation.rotation) as GameObject;

        // Ignore collision with object firing the projectile
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());

        // Launch projectile
        Rigidbody rb = projectile.AddComponent<Rigidbody>();
        rb.velocity = projectile.transform.forward * 10;
        rb.useGravity = false;

        // Destroy projectile after 5 seconds
        Destroy(projectile, fireBallLife);
    }
}
