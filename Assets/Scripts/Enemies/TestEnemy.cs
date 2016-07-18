using UnityEngine;
using System.Collections;

public class TestEnemy : MonoBehaviour, Damagable, IEnemy, Movable {

	[SerializeField]
	private Transform target;

    private Animator anim;
    private Rigidbody rb;

	private int health;
	private int speed;
	private string name;
	private EnemyTypes type;

	private int damage;
	private float attackRange;
	private bool canAttack;
    private bool isDead;


	public int Health 		{ get { return health;} set{health = value;} }
	public int Speed 		{ get  {return speed;} 	set{speed = value;} }
	public string Name 		{ get { return name;} 	set{name = value;} }
	public EnemyTypes Type 	{ get { return type;} 	set{type = value;} }
	public Vector3 Position { get { return transform.position; } set { transform.position = value; } }

	// Use this for initialization
	void Awake () {
		health = 10;
		speed = 5;
		name = "Randy";
		type = EnemyTypes.WALKER;
		canAttack = false;
		attackRange = 1.5f;
		damage = 1;
        target = GameObject.FindGameObjectWithTag("Door").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead)
        {
            Move();
            Attack();
        }
		
	}

	public void Attack() {
		if (canAttack) {
            Manager.Get().info.GateHealth -= damage;
			//Debug.Log (GameInfo.GateHealth);
		}

	}

	public void TakeDamage(int dmg) {
        
        health -= dmg;
        anim.Play("Damage");
        if (health <= 0)
        {
            OnDeath();
        }
	}

	public void OnDeath() {
        anim.Play("Death");
        rb.isKinematic = true;
        rb.detectCollisions = false;
        isDead = true;
        StartCoroutine(Death());
		Debug.Log ("Destroyed " + name);
	}

	public void Move () {
		
		if (Vector3.Distance (transform.position, new Vector3(target.position.x, 1, target.position.z)) >= attackRange) {
			var dir = (target.position - transform.position).normalized;
			Position += dir * speed * Time.deltaTime;
			Position = new Vector3 (Position.x, 1, Position.z);
			//transform.LookAt (target.transform);
			//transform.rotation = Quaternion.Euler (0, transform.rotation.y, transform.rotation.z);
			canAttack = false;
		} else {
			canAttack = true;
		}

	}

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }
}
