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
    //private GameObject[] spawns;
    //private float timeToNextSpawn = float.MaxValue;

	public GameObject healthbar;


    public AudioClip fadeinMusic;
    public AudioClip mainMusic;


	// Use this for initialization
	void Start () {
        instance = this;
        playerLiving = player.GetComponent<LivingEntity>();

        SoundManager.CrossfadeMusic(fadeinMusic, 2.5f);
        SoundManager.PlayNext(mainMusic, 2.6f); //let the fade finish before we use the (now) empty AudioSource

        //spawns = GameObject.FindGameObjectsWithTag("Respawn");
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

        /*
        if(timeToNextSpawn<=0){
            while(true){
                Vector3 pos = spawns[Random.Range(0, spawns.Length)].transform.position;
                if (Vector3.Distance(pos, player.transform.position) > 10){
                    Instantiate<GameObject>(enemies[Random.Range(0, enemies.Length)], pos, new Quaternion());
                    ResetTimer();
                    break;
                }
            }
        }
        timeToNextSpawn-=1/60f;
        */
    }
    /*
    void ResetTimer(){
        float newTime = Mathf.Pow(0.9947f, score) * 17.0509f;
        if (timeToNextSpawn < 0)
            timeToNextSpawn = float.MaxValue;
        timeToNextSpawn = Mathf.Min(newTime, timeToNextSpawn);
    }
    */
    public static void AddScore(int score) {
        instance.score += score;
        instance.scoreDisplay.text = ("SCORE: " + instance.score);
        //GameObject.Find("Game Controller").GetComponent<GameController>().ResetTimer();
    }
}
