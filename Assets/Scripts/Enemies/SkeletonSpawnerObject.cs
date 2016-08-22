using UnityEngine;
using System.Collections;
using VRTK;

public class SkeletonSpawnerObject : VRTK_InteractableObject {

    private EnemySpawner spawner;
    [Header("Spawner")]
    public int NumberOfEnemies = 1;

    protected override void Awake()
    {
        base.Awake();
        spawner = new EnemySpawner(Resources.Load("Enemy/Skeleton") as GameObject);
    }

    public override void StartUsing(GameObject currentUsingObject)
    {
        base.StartUsing(currentUsingObject);
        spawner.Spawn(NumberOfEnemies);
    }
}
