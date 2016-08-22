using UnityEngine;
using System.Collections;
using VRTK;

public class BaseSpell : MonoBehaviour {

    protected Transform firingLocation;
    protected BasePointer pointer;

    public virtual void CastSpell(object sender, ControllerInteractionEventArgs e)
    {
        
        Debug.Log("Hi from base");
    }

    

    public virtual void LaunchProjectile(GameObject obj, Vector3 force, float life)
    {
        // Instantiate projectile at preset firinglocation
        GameObject projectile = Instantiate(obj, firingLocation.position, firingLocation.rotation) as GameObject;

        // Ignore collision with object firing the projectile
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());

        // Launch projectile
        Rigidbody rb = projectile.AddComponent<Rigidbody>();
        rb.AddForce(force);

        // Add damage script to hurt objects with a damagable script on them
        projectile.AddComponent<Damage>();

        // Destroy projectile after life seconds
        Destroy(projectile, life);


    }

    public virtual GameObject LaunchProjectile(GameObject obj, Vector3 force, float life, int damage)
    {
        // Instantiate projectile at preset firinglocation
        GameObject projectile = Instantiate(obj, firingLocation.position, firingLocation.rotation) as GameObject;

        // Ignore collision with object firing the projectile
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());

        // Launch projectile
        Rigidbody rb = projectile.AddComponent<Rigidbody>();
        rb.AddForce(force);

        // Add damage script to hurt objects with a damagable script on them
        projectile.AddComponent<Damage>().damage = damage;

        // Destroy projectile after 5 seconds

        Destroy(projectile, life);

        return projectile;


    }

    public virtual GameObject SpawnObjectInFront(GameObject obj, float distance, float life)
    {
        GameObject newObj = Instantiate(obj, firingLocation.transform.position + firingLocation.forward * distance, firingLocation.rotation) as GameObject;
        Destroy(newObj, life);
        return newObj;
    }

    public virtual GameObject SpawnObjectAtPointer(GameObject obj)
    {
        if (pointer.IsValid())
            return Instantiate(obj, pointer.GetPointerArgs().position, Quaternion.identity) as GameObject;
        return null;
    }

    public virtual void InitPointer()
    {
        if (firingLocation != null)
        {
            GameObject prefab = Resources.Load("Pointer/PointerContainer") as GameObject;
            Debug.Log(prefab.transform.rotation);
            GameObject ptr = Instantiate(prefab, firingLocation.position, Quaternion.identity) as GameObject;
            ptr.transform.SetParent(transform);
            ptr.transform.localRotation = Quaternion.Euler(0, 0, 0);
            pointer = ptr.GetComponent<BasePointer>();
        }
        
    }

    public virtual void DestroyPointer()
    {
        if (pointer != null)
        {
            Destroy(pointer.gameObject);
            pointer = null;
        }
       

    }

    public virtual void Awake()
    {
        var events = GetComponentInParent<VRTK_ControllerEvents>();
        if (events == null)
        {
            Debug.Log("Controller events is null. Calling from BaseSpell" + gameObject.name);
            return;
        }

        events.TriggerPressed += new ControllerInteractionEventHandler(CastSpell);

        var location = GetComponentInChildren<ProjectileFiringLocation>();
        if (location == null)
        {
            Debug.Log("Object does not have a ProjectileFiringLocation attached");
            return;
        }
        firingLocation = location.FiringLocation;

        



    }

    public virtual void OnDestroy()
    {
        var events = GetComponentInParent<VRTK_ControllerEvents>();
        if (events == null)
        {
            Debug.Log("Controller events is null. Calling from BaseSpell");
            return;
        }

        events.TriggerPressed -= new ControllerInteractionEventHandler(CastSpell);

        DestroyPointer();
    }


    
   
}
