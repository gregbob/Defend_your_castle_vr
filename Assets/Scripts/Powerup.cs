using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private ParticleSystem powerupParticle;

    private bool powered = false;

    WandController controller;

	// Use this for initialization
	void Start () {
        powerupParticle.Clear();
        powerupParticle.Stop();
        controller = GetComponent<WandController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (controller.TriggerButtonDown)
        {
            if (!powerupParticle.isPlaying)
            {
                powerupParticle.Play();
                Manager.Get().info.powered = true;
            } else
            {
                powerupParticle.Clear();
                powerupParticle.Stop();
                Manager.Get().info.powered = false;
            }
            
            
        }
	}

}
