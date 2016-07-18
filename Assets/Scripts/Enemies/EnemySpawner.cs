using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SpawnArea {
	public Vector2 p1 { get; set; }
	public Vector2 p2 { get; set; }
}

public class EnemySpawner  {

	/*
	 *  Add more complicated logic about spawn areas. Could pass in a custom object
	 * detailing how to do this. SpawnLogic
	 * 
	 */



	//private Component enemyScript;
	private GameObject enemy;
    private Transform parent;
	private float minSpawnRate, maxSpawnRate;
	private int numSpawn;
	private List<GameObject> allEnemies;
	private bool done;

	public bool Done { get { return done; } set { done = value; } }

	public EnemySpawner(ref List<GameObject> allEnemies, GameObject enemy, Transform parent, float minRate, float maxRate, int numSpawn) {
		//enemyScript = script;
		this.enemy = enemy;
        this.parent = parent;
		minSpawnRate = minRate;
		maxSpawnRate = maxRate;
		this.numSpawn = numSpawn;
		this.allEnemies = allEnemies;
		done = false;
	}

	public IEnumerator SpawnEnemy() {
		int spawned = 0;
		while (spawned < numSpawn) {
			var rand = Random.Range (minSpawnRate, maxSpawnRate);
			yield return new WaitForSeconds (rand);
            var randPos = Random.Range(-20, 20);

            // Set enemy to be under the Enemies parent object in the hierarchy
			var go = (GameObject) UnityEngine.Object.Instantiate (enemy, new Vector3(10,0,randPos), enemy.transform.rotation);
            go.transform.SetParent(parent);

            // Add newly spawned enemy to list of currently alive enemies
            allEnemies.Add(go);
			spawned += 1;
		}
		done = true;

	}
}
