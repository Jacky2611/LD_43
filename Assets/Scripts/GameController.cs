using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject player;
    LivingEntity playerLiving;


    public Slider healthbarLeft;
    public Slider healthbarRight;

	// Use this for initialization
	void Start () {
        playerLiving = player.GetComponent<LivingEntity>();
	}
	
	// Update is called once per frame
	void Update () {

        //TODO transitions

        healthbarLeft.value = playerLiving.health / playerLiving.maxHealth;
        healthbarRight.value = playerLiving.health / playerLiving.maxHealth;

    }

    void FixedUpdate()
    {
        if (player == null)
        {
            //game over

            Debug.Log("Game over");
        }
    }
}
