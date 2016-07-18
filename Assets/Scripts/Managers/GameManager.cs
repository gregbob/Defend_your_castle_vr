using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public enum GameState {MAINMENU, WAVE, PAUSED, LEVELOVER, GAMEOVER}
	public GameState gameState;
	public GameState prevState;

	public bool WaveStart {
		get {
			if (gameState.Equals (GameManager.GameState.WAVE) &&
			    (prevState.Equals (GameManager.GameState.MAINMENU) ||
			    (prevState.Equals (GameManager.GameState.LEVELOVER)))) {
				return true;
			}
			return false;
		}
	}

	public bool Paused { get { return gameState.Equals (GameState.PAUSED); } }
	public bool MainMenu { get { return gameState.Equals (GameState.MAINMENU); } }
	public bool Wave { get { return gameState.Equals (GameState.WAVE); } }
	public bool LevelOver { get { return gameState.Equals (GameState.LEVELOVER); } }
	public bool GameOver { get { return gameState.Equals (GameState.GAMEOVER); } }

	// Define event to be called whenver the game state is changed
	public delegate void GameStateChange();
	public event GameStateChange OnGameStateChange;



	public void ChangeState(GameState state) {
		prevState = gameState;
		gameState = state;

		if (OnGameStateChange != null)
			OnGameStateChange ();
	}
		

	// Use this for initialization
	void Start () {
		ChangeState (GameState.MAINMENU);
	}

}
