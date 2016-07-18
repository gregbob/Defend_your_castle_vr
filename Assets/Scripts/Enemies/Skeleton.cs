using UnityEngine;
using System.Collections;

public class Skeleton : BaseEnemy {

	private NavMeshAgent agent;

	void Awake()
    {
        health = 10;
        speed = 2;
        attackRange = 1.5f;
        damage = 1;
		name = "Skeleton";
		type = EnemyTypes.SKELETON;
        target = GameObject.FindGameObjectWithTag("Door").transform;
		agent = GetComponent<NavMeshAgent> ();
		//canAttack = false;
        //anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        //isDead = false;

    }

    void Update()
    {
        if (!isDead)
        {
            Move();
            Attack();
        }

    }

	public override void Move ()
	{
		agent.destination = target.position;
	}
}
