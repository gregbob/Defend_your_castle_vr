using UnityEngine;
using System.Collections;

public class ShootRay : MonoBehaviour {

    [SerializeField]
    private ParticleSystem muzzleFlash;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Shoot(100);
	}

    public void Shoot(float distance)
    {

        RaycastHit hit;
        muzzleFlash.Stop();
        muzzleFlash.Play();
        if (Physics.Raycast(transform.position, transform.right * -1 * distance, out hit))
        {
            Debug.Log("Name " + hit.collider.gameObject.name);
            Damagable damagable = hit.collider.gameObject.GetComponent<Damagable>();
            if (damagable!= null)
            {
                Debug.Log("Name " + hit.collider.gameObject.name);
                damagable.TakeDamage(5);
            }
        }

    }


}
