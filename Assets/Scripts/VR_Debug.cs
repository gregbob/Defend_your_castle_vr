using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VR_Debug : MonoBehaviour {

    public Canvas canvas;
    private Image panel;

	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
        canvas.transform.position = Camera.main.transform.position;
        canvas.transform.position += Camera.main.transform.forward ;
        canvas.transform.LookAt(Camera.main.transform);
	}
}
