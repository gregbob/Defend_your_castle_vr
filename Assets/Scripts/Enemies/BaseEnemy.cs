using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]

public class BaseEnemy : MonoBehaviour, Damagable, Movable
{
    #region Fields
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
	[SerializeField]
	protected float attackSpeed;

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
	[SerializeField]
	protected bool attackOnCooldown;

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
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        isDead = false;
		attackOnCooldown = false;
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
		StartCoroutine (Death ());
    }

    public virtual void Attack()
    {
		if (Vector3.Distance (transform.position, new Vector3 (target.position.x, 1, target.position.z)) <= attackRange) {
           
			//Manager.Get().info.GateHealth -= damage;
		//	Debug.Log (Manager.Get().info.GateHealth);
		}
            

    }

    public virtual void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Debug.Log("HIT");
            OnDeath();
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }

	IEnumerator AttackCooldown() {
		if (!attackOnCooldown) {
			//Attack
			attackOnCooldown = true;
			yield return new WaitForSeconds (attackSpeed);
			attackOnCooldown = false;
		} else {
			yield return null;
		}
	}

}
