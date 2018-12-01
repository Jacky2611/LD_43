using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {


    public float lifetime = 50f;
    public int maxBounceCounter=4;
    private int bounceCounter;


    void Awake()
    {
        bounceCounter = 0;
        Destroy(gameObject, lifetime);  //just in case we go out of bounds
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
            audioSource.Play();

        */
        bounceCounter++;
        

        if (bounceCounter > maxBounceCounter)
        {
            Destroy(gameObject);
        }

    }
}
