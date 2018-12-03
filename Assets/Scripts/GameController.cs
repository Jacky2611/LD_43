using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private static GameController instance;

    public GameObject player;
    LivingEntity playerLiving;
    public Text scoreDisplay;

    private int score;


	public GameObject healthbar;


    public AudioClip fadeinMusic;
    public AudioClip mainMusic;


	// Use this for initialization
	void Start () {
        instance = this;
        playerLiving = player.GetComponent<LivingEntity>();

        SoundManager.CrossfadeMusic(fadeinMusic, 1.5f);
        SoundManager.PlayNext(mainMusic);
        //SoundManager.CrossfadeMusic(mainMusic, 0.1f, fadeinMusic.length-0.1f);
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

    public static void AddScore(int score) {
        instance.score += score;
        instance.scoreDisplay.text = ("SCORE: " + instance.score);

    }
}
