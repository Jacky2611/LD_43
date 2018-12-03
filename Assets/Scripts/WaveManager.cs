using SubjectNerd.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour {

    [Reorderable]
    public Wave[] waves;
    int currentWave = 0;
    List<GameObject> spawnedEnemies;

    bool wavesOngoing;



    GameObject[] spawnLocations;
    GameObject player;

    public UnityEvent doAfterLastWave;
    [Header("If no custom action is picked a Scene with this name will be loaded:")]
    public String nextScene;

    // Use this for initialization
    void Start () {
        spawnLocations = GameObject.FindGameObjectsWithTag("Respawn");
        spawnedEnemies = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        wavesOngoing = true;
        NextWave();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (wavesOngoing)
        {
            spawnedEnemies.RemoveAll(item => item == null || (item.GetComponent<LivingEntity>() == null || item.GetComponent<LivingEntity>().health<=0));
                
            if(spawnedEnemies.Count <= 0)
            {
                NextWave();
            }
        }

	}

    void NextWave()
    {
        if(currentWave == waves.Length)
        {
            wavesOngoing = false;
            DefeatedLastWave();
            return;
        }

        Wave w = waves[currentWave];

        foreach(Enemies enemies in w.enemies)
        {
            for(int i = 0; i<enemies.number; i++)
            {
                List<GameObject> viableSpawnLocation = new List<GameObject>();//[spawnLocations.];// = spawnLocations.
                viableSpawnLocation.AddRange(spawnLocations);
                viableSpawnLocation.RemoveAll(location => Vector3.Distance(location.transform.position, player.transform.position) < 10);

                //System.Array.Copy(spawnLocations,viableSpawnLocation,spawnLocations.Length);
                Vector3 pos = viableSpawnLocation[Random.Range(0, viableSpawnLocation.Count-1)].transform.position;
                GameObject go = Instantiate(enemies.enemy, pos, new Quaternion());
                spawnedEnemies.Add(go);
            }
        }
        

        currentWave++;
    }


    void DefeatedLastWave()
    {
        Debug.Log("Defeated Last Wave");

        if(doAfterLastWave == null || doAfterLastWave.GetPersistentEventCount()<=0)
        {
            Debug.Log("IS null");

            if (String.IsNullOrEmpty(nextScene))
            {
                StartCoroutine(LoadGameScene("MainMenu"));
            } else
            {
                StartCoroutine(LoadGameScene(nextScene));
            }

        } else
        {
            doAfterLastWave.Invoke();
        }
    }

    IEnumerator LoadGameScene(String load)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(load);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    [Serializable]
    public struct Wave
    {
        [Reorderable]
        public Enemies[] enemies;
    }

    [Serializable]
    public struct Enemies
    {
        public GameObject enemy;
        public int number;
    }
}
