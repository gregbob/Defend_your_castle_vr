using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {
    private GameObject enemy;
	int level;
	List<GameObject> allEnemies;
	List<EnemySpawner> allSpawners;
    GameObject enemyParent;

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
			//Debug.Log ("Main. Wave.");
			Reset ();
		}
		if (gm.WaveStart) {

			//Debug.Log ("WaveStart. Wave.");
			NextLevel ();
		}  
		if (gm.Wave) {
			//Debug.Log("Wave. Wave.");
		}  
		if (gm.Paused) {
			//Debug.Log("Paused. Wave.");
		}  
		if (gm.LevelOver) {
			//Debug.Log("LevelOver. Wave.");
		} 
		if (gm.GameOver) {
			//Debug.Log("GameOver. Wave.");
			DestroyEnemies ();
		}
	}

	// Use this for initialization
	void Awake () {
        enemy = Resources.Load("Enemy/Skeleton") as GameObject;
		level = 0;
		allEnemies = new List<GameObject> ();
		allSpawners = new List<EnemySpawner> ();	
	}

	void Update () {
		if (Manager.Get ().gm.Wave) {
			if (LevelComplete ()) {
				Manager.Get ().gm.ChangeState (GameManager.GameState.LEVELOVER);
			}
			if (GameOver ()) {
				Manager.Get ().gm.ChangeState (GameManager.GameState.GAMEOVER);
			}
		}

	}

	public bool GameOver() {
		return Manager.Get ().info.GateHealth <= 0;
	}

	/// <summary>
	/// Checks to see if the current level is complete. If no enemies are alive, check
	/// to see if all spawners are done too. If all enemies are dead, and all spawners
	/// are done, level is complete.
	/// </summary>
	/// <returns><c>true</c>, if complete was leveled, <c>false</c> otherwise.</returns>
	public bool LevelComplete() {
		bool allSpawnersDone = false;

		if (allEnemies.Count > 0) {
			return false;
		}
		foreach (EnemySpawner spawner in allSpawners) {
			allSpawnersDone = spawner.Done;
		}
		return allSpawnersDone;
	}

	public void NextLevel() {
		level += 1;
		InitLevel (level);
		Debug.Log ("Current wave " + level);
	}

	private void InitLevel(int level) {
		allEnemies.Clear ();
		allSpawners.Clear ();

		CreateParent ();
		allSpawners.Add(new EnemySpawner (ref allEnemies, enemy, enemyParent.transform,1f, 2f, 10));

		foreach(EnemySpawner spawner in allSpawners)  {
			StartCoroutine (spawner.SpawnEnemy ());
		}

	}

	void Reset() {
		allEnemies.Clear ();
		allSpawners.Clear ();
		level = 0;
		Manager.Get ().info.GateHealth = 100;
	}

	private void DestroyEnemies() {
		Destroy (enemyParent);
		enemyParent = null;
	}

	private void CreateParent() {
		if (enemyParent == null) {
			enemyParent = new GameObject();
			enemyParent.name = "Enemies";
		}
	}

	void OnGUI () {
		if (GUI.Button (new Rect (0, 0, 100, 30), "Next level")) {
			AdvanceLevel ();
		}
	}

	private void AdvanceLevel() {
		if (!Manager.Get ().gm.LevelOver) {
			foreach (EnemySpawner spawner in allSpawners) {
				spawner.Done = true;
			}
			StopAllCoroutines ();
			DestroyEnemies ();
			allEnemies.Clear ();
			if(LevelComplete()) {
				Manager.Get().gm.ChangeState(GameManager.GameState.LEVELOVER);
			}

		}

	}
}
