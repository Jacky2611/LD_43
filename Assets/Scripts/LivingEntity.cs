using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LivingEntity : MonoBehaviour {

    public float maxHealth = 100;

    public float health;



    // Use this for initialization
    void Awake()
    {
        this.health = maxHealth;
    }




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (this.health <= 0)
        {
            //TODO: Animate this
            Destroy(gameObject);

        }
	}

    public void TakeDamage(float damage)
    {
        //Debug.Log("Talking Damage");
        this.health -= Random.Range((damage*0.85f), damage); //take a random amount of damage
    }

   
}
