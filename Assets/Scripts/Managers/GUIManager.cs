using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	[Header("Canvases")]
	public GameObject mainMenu;
	public GameObject gameOver;
	public GameObject waveGUI;
	public GameObject pauseMenu;
	public GameObject levelOver;
	[Header("Buttons")]
	public Button b_Play;
	public Button b_PlayAgain;
	public Button b_NextWave;

	// Use this for initialization
	void Start () {
		b_Play.onClick.AddListener (() => {
			Play ();
		});
		b_PlayAgain.onClick.AddListener (() => {
			PlayAgain ();
		});
		b_NextWave.onClick.AddListener (() => {
			NextWave ();
		});
	}

	void OnEnable () {
		Manager.Get ().gm.OnGameStateChange += StateManager;
	}

	void OnDisable () {
		Manager.Get ().gm.OnGameStateChange -= StateManager;
	}

	void StateManager () {
		GameManager gm = Manager.Get ().gm;
		if (gm.MainMenu) {
			// don't do anything
			//Debug.Log ("Main. GUI");
			OnlyShow (mainMenu);
		} 
		if (gm.WaveStart) {
			// Show wave GUI
			//Debug.Log ("WaveStart. GUI");
		} 
		if (gm.Wave) {
			//Debug.Log ("Wave. GUI");
			OnlyShow (waveGUI);
		} 
		if (gm.Paused) {
			//Debug.Log ("Paused. GUI");
			OnlyShow (pauseMenu);
		}  
		if (gm.LevelOver) {
			//Debug.Log ("LevelOver. GUI");
			OnlyShow (levelOver);
		}  
		if (gm.GameOver) {
			//Debug.Log ("GameOver. GUI");
			OnlyShow (gameOver);
		}
	}

	void Play() {
		Manager.Get ().gm.ChangeState (GameManager.GameState.WAVE);
        Debug.Log("play");
	}

	void PlayAgain() {
		Manager.Get ().gm.ChangeState (GameManager.GameState.MAINMENU);
	}

	void NextWave() {
		Manager.Get ().gm.ChangeState (GameManager.GameState.WAVE);

	}

	void OnlyShow(GameObject objToShow) {
		GameObject[] objs = { gameOver, mainMenu, pauseMenu, waveGUI, levelOver };
		for (int i = 0; i < objs.Length; i++) {
			if (objs[i].Equals (objToShow)) {
				objs[i].SetActive (true);
			} else {
				objs[i].SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
