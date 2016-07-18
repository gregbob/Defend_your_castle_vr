using UnityEngine;
using System.Collections;

public class ShootRay : MonoBehaviour {

    [SerializeField]
    private ParticleSystem muzzleFlash;

    private WandController controller;

    private float distance = 100;

	// Use this for initialization
	void Awake() {
        controller = GetComponent<WandController>();
	}

    void OnEnable()
    {
        controller.OnTriggerDown += Shoot;
    }
	
	// Update is called once per frame
	void Update () {
        //Shoot(100);
	}



    public void Shoot()
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
