using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LivingEntity : MonoBehaviour {


    private BloodSplatter blood;
    public float maxHealth = 100;

    public float health;

    public AudioClip[] hurtSounds;

    public AudioClip[] deathSounds;



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

        
	}

    public void TakeDamage(float damage)
    {
        if (blood != null)
        {
            blood.HitBlood();
        }
        GetComponent<Animator>().SetTrigger("hit");

        //Debug.Log("Talking Damage");
        this.health -= Random.Range((damage*0.85f), damage); //take a random amount of damage
		this.health = Mathf.Max(health, 0);

		if(health>0)
	        SoundManager.PlayRandomAt(hurtSounds, transform.position);
		else
		{
			//TODO: Animate this
			if (blood != null)
			{
				blood.DeathBlood();
			}
			SoundManager.PlayRandomAt(deathSounds, transform.position);
			GetComponent<Animator> ().SetTrigger ("die");
			gameObject.isStatic = true;
            if(tag == "Player"){
                transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                GetComponent<PlayerController>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
            }
            else{
                GetComponent<EnemyControler>().Die();
            }
		}
    }
}
