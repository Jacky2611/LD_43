using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour {

    public float lifetime = 100f;

    public GameObject[] splatterDeath;
    public float splatterDeathDistance = .5f;
    public int minDeathBlood = 2;
    public int maxDeathBlood = 5;


    public GameObject[] splatterHurt;
    public float splatterHurtDistance = .4f;
    public int minHurtBlood = 2;
    public int maxHurtBlood = 5;

    public void HitBlood()
    {
        CreateSplatter(minHurtBlood, maxHurtBlood, splatterHurt, splatterHurtDistance);
    }

    public void DeathBlood()
    {
        CreateSplatter(minDeathBlood,maxDeathBlood, splatterDeath, splatterDeathDistance);
    }


    private void CreateSplatter(int min, int max, GameObject[] splats, float splatterDistance)
    {
        if (splats.Length > 0) {
            int splatterCount = Random.Range(min, max);
            for (int i = 0; i < splatterCount; i++)
            {

                Vector2 randomCircle = Random.insideUnitCircle*splatterDistance;

                Vector3 pos = new Vector3(transform.position.x+randomCircle.x, transform.position.y+randomCircle.y, 0);

                //pos += new Vector3(Random.value* splatterDistance- (splatterDistance/2), Random.value* splatterDistance-(splatterDistance/2), 0);

                GameObject o = Instantiate(splats[Random.Range(0, splats.Length)], pos, Quaternion.Euler(0, 0, Random.value * 360));
                Destroy(o, lifetime);
            }
        }
    }
}
