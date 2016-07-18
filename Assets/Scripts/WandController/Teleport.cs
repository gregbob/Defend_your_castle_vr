using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private Transform Steam_vr;

    private WandController controller;

    int pos = 0; 

	// Use this for initialization
	void Start () {
        controller = GetComponent<WandController>();
	}

    //void TeleportNext()
    //{
    //    Debug.Log("Teleport");
    //    Steam_vr.transform.position = waypoints[pos].position;
    //    pos += 1;
    //    if (pos == waypoints.Length)
    //    {
    //        pos = 0;
    //    }
    //}
    public void TeleportTo(int loc)
    {
        if (loc < waypoints.Length)
        {
            
            Steam_vr.transform.position = waypoints[loc].position;

        }
        


    }

}
