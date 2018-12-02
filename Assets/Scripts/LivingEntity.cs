using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LivingEntity : MonoBehaviour {


    private BloodSplatter blood;
    public float maxHealth = 100;

    public float health;



    // Use this for initialization
    void Awake()
    {
        this.health = maxHealth;
        blood = gameObject.GetComponent<BloodSplatter>();

    }




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (this.health <= 0)
        {
            //TODO: Animate this
            if (blood != null)
            {
                blood.DeathBlood();
            }

            Destroy(gameObject);

        }
	}

    public void TakeDamage(float damage)
    {
        if (blood != null)
        {
            blood.HitBlood();
        }

        //Debug.Log("Talking Damage");
        this.health -= Random.Range((damage*0.85f), damage); //take a random amount of damage
    }

   
}
