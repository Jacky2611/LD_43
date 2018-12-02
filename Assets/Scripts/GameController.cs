using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject player;
    LivingEntity playerLiving;


	public GameObject healthbar;

	// Use this for initialization
	void Start () {
        playerLiving = player.GetComponent<LivingEntity>();
	}
	
	// Update is called once per frame
	void Update () {

        //TODO transitions

		healthbar.transform.localScale = new Vector3(playerLiving.health / playerLiving.maxHealth,1);

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
