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

    private bool death;

    // Use this for initialization
    void Awake()
    {
        this.death = false;
        this.health = maxHealth;
        blood = gameObject.GetComponent<BloodSplatter>();

    }

    public void FixedUpdate()
    {
        if (health <= 0 && !death) {
            death = true;
            //We are dead

            //TODO: Animate this
            if (blood != null)
            {
                blood.DeathBlood();
            }
            SoundManager.PlayRandomAt(deathSounds, transform.position);
            GetComponent<Animator>().SetTrigger("die");
            gameObject.isStatic = true;
            if (tag == "Player")
            {
                transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                GetComponent<PlayerController>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                Invoke("Reload", 3);
            }
            else
            {
                GetComponent<EnemyControler>().Die();
                GameController.AddScore((int)Random.Range(maxHealth * .9f, maxHealth * 1.1f));

            }
        }
    }
    public void Reload(){
        Application.LoadLevelAsync("Level");
    }

    public void LooseHealth(float healthLoss)
    {
        this.health -= Random.Range((healthLoss * 0.85f), healthLoss); //take a random amount of damage
        this.health = Mathf.Max(health, 0);
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


        SoundManager.PlayRandomAt(hurtSounds, transform.position);
        if (tag == "Player")
        {
            gameObject.GetComponent<Animator>().SetBool("harvest", false);
        }

    }
    public void OnTriggerStay2D(Collider2D collider)
    {
        if (health.Equals(0.0f) && collider.gameObject.tag == "Player" && Input.GetButtonDown("Harvest"))
        {
            collider.gameObject.GetComponent<PlayerController>().StartHarvest(gameObject);
            collider.gameObject.GetComponent<Animator>().SetBool("harvest", true);
        }else if(health.Equals(0.0f) && collider.gameObject.tag == "Player" && Input.GetButtonUp("Harvest"))
        {
            collider.gameObject.GetComponent<PlayerController>().AbortHarvest();
            collider.gameObject.GetComponent<Animator>().SetBool("harvest", false);
        }
    }
    public void GainHealth(float amount){
        health += amount;
        health = Mathf.Min(maxHealth, health);
    }
    public void HurtSound()
    {
        SoundManager.PlayRandomAt(hurtSounds, transform.position);
    }
}
