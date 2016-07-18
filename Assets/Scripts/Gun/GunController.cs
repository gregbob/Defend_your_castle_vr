using UnityEngine;
using System.Collections;

[RequireComponent (typeof(WandController))]
[RequireComponent (typeof(AudioSource))]

public class GunController : MonoBehaviour {


    private WandController controller;
    private ShootRay shoot;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip shootSound;

	// Use this for initialization6
	void Start () {
        controller = GetComponent<WandController>();
        shoot = GetComponentInChildren<ShootRay>();
        audioSource = GetComponent<AudioSource>();
        shootSound = Resources.Load("Sounds/g_shoot1") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
	    if (controller.TriggerButtonDown)
        {
            Debug.Log("Shooting");
            shoot.Shoot(100);
            audioSource.clip = shootSound;
            audioSource.Play();
            controller.Vibrate(.2f);
        }
	}
}
