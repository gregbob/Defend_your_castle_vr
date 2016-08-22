using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class Skeleton : BaseEnemy {

	private NavMeshAgent agent;
    private Animator anim;

	void Awake()
    {
        health = 10;
        speed = 2;
        attackRange = 1.5f;
        damage = 1;
		name = "Skeleton";
		type = EnemyTypes.SKELETON;
        target = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent> ();
        //canAttack = false;
        anim = GetComponent<Animator>();
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

    public override void OnDeath()
    {
        base.OnDeath();
        anim.Play("Death");
        agent.Stop();
        
    }

    public override void Move ()
	{
		agent.destination = target.position;
	}
}
