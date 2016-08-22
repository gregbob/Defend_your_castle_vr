using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

    public int damage = 10;


    void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name + " collided with " + other.gameObject.name + " calling from damage.");
        if (other.collider.gameObject.GetComponent<Damagable>() != null)
        {
            other.collider.gameObject.GetComponent<Damagable>().TakeDamage(damage);
        }
    }
  
}
