using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof (AudioSource))]
public class ObjectSwingSound : MonoBehaviour {

   

    [SerializeField]
    private float reqSpeed;

    [SerializeField]
    private AudioClip[] swingSounds;

    private AudioSource source;

    private Vector3 prevPos;

    private Queue<Vector3> posQueue;


	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        posQueue = new Queue<Vector3>();
	}

    // Update is called once per frame
    void Update() {
        posQueue.Enqueue(transform.position);
        if (posQueue.Count > 2)
        {
            posQueue.Dequeue();
            if (MovingFastEnough())
            {
                PlaySound();
            }

        }
        
	}

    void LateUpdate()
    {
      //  Debug.Log((transform.position - prevPos).magnitude / Time.deltaTime);
    }

    private bool MovingFastEnough()
    {
        var speed = (posQueue.ElementAt(1) - posQueue.ElementAt(0)).magnitude / Time.deltaTime;

        if (speed >= reqSpeed)
        {
            return true;
        }
        return false;
    }

    private void PlaySound()
    {
        if (!source.isPlaying)
        {
            source.clip = GetRandomSound();
            source.Play();
        }
    }

    private AudioClip GetRandomSound()
    {
        return swingSounds[Random.Range(0, swingSounds.Length)];
    }
}
