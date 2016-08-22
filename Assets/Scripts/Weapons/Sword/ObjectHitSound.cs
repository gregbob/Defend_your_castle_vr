using UnityEngine;
using System.Collections;


[RequireComponent (typeof(AudioSource))]
public class ObjectHitSound : MonoBehaviour {

    private AudioSource source;

    [SerializeField]
    private AudioClip[] hitSounds;

    private WandController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<WandController>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("Hit " + coll.gameObject.name );
        Damagable dmgble = coll.gameObject.GetComponent<Damagable>();
        if (dmgble != null)
        {
            Debug.Log("Hit damagble");
            dmgble.TakeDamage(10);
            PlaySound();
            controller.Vibrate(.25f);
        }
       

    }

    private void PlaySound()
    {

        source.clip = GetRandomSound();
        source.Play();
        
    }

    private AudioClip GetRandomSound()
    {
        return hitSounds[Random.Range(0, hitSounds.Length)];
    }

}
