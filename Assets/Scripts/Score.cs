using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    [SerializeField]
    private Text scoreText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "SCORE: " + Manager.Get().info.score;
	}
}
