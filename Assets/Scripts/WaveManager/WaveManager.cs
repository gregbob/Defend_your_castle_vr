using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {
    private GameObject enemy;
	int level;
	List<GameObject> allEnemies;
	List<EnemySpawner> allSpawners;
    GameObject enemyParent;


	// Use this for initialization
	void Awake () {
        enemy = Resources.Load("Enemy/Skeleton") as GameObject;
        enemyParent = new GameObject();
        enemyParent.name = "Enemies";
		level = 0;
		allEnemies = new List<GameObject> ();
		allSpawners = new List<EnemySpawner> ();	
		InitLevel (level);

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
	}

	private void InitLevel(int level) {
		allEnemies.Clear ();
		allSpawners.Clear ();


		allSpawners.Add(new EnemySpawner (ref allEnemies, enemy, enemyParent.transform,1f, 2f, 15));

		foreach(EnemySpawner spawner in allSpawners)  {
			StartCoroutine (spawner.SpawnEnemy ());
		}

	}
}
