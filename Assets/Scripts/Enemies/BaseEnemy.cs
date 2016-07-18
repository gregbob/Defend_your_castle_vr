using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class BaseEnemy : MonoBehaviour, Damagable, Movable
{
    #region Fields
    protected Animator anim;
    protected Rigidbody rb;
 
    [Header("Stats")]
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float attackRange;

    [Header("Identifiers")]
    [SerializeField]
    protected string name;
    [SerializeField]
    protected EnemyTypes type;

    [Header("Booleans")]
    [SerializeField]
    protected bool canAttack;
    [SerializeField]
    protected bool isDead;

    [Header("Attack Goal")]
    [SerializeField]
    protected Transform target;

    protected Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    #endregion 
    //public int Health { get { return health; } set { health = value; } }
    //public int Speed { get { return speed; } set { speed = value; } }
    //public int Damage { get { return damage; } set { damage = value; } }
    //public string Name { get { return name; } set { name = value; } }
    //public EnemyTypes Type { get { return type; } set { type = value; } }
    //public Transform target { get { return target; } set { target = value; } }
    //public float AttackRange { get { return attackRange; } set { attackRange = value; } }


    // Use this for initialization
    void Awake()
    {
        health = 1;
        speed = 3;
        name = "Base";
        type = EnemyTypes.WALKER;
        canAttack = false;
        attackRange = 1.5f;
        damage = 1;
        target = GameObject.FindGameObjectWithTag("Door").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isDead = false;
    }

    public virtual void Move()
    {
        if (Vector3.Distance(transform.position, new Vector3(target.position.x, 1, target.position.z)) >= attackRange)
        {
            var dir = (target.position - transform.position).normalized;
            Position += dir * speed * Time.deltaTime;
            Position = new Vector3(Position.x, 1, Position.z);
            //transform.LookAt (target.transform);
            //transform.rotation = Quaternion.Euler (0, transform.rotation.y, transform.rotation.z);
            canAttack = false;
        }
        else {
            canAttack = true;
        }
    }

    public virtual void OnDeath()
    {
        isDead = true;
        Debug.Log("Dead from base class");
    }

    public virtual void Attack()
    {
        if (canAttack)
        {
            Manager.Get().info.GateHealth -= damage;
            //Debug.Log (GameInfo.GateHealth);
        }
    }

    public virtual void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }

}
