using UnityEngine;
using System.Collections;

public class Skeleton : BaseEnemy {

	void Awake()
    {
        health = 10;
        speed = 2;
        name = "Skeleton";
        type = EnemyTypes.WALKER;
        canAttack = false;
        attackRange = 1.5f;
        damage = 1;
        target = GameObject.FindGameObjectWithTag("Door").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isDead = false;
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
            Attack();
        }

    }
}
