using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 2f;


    public GameObject projectile;
    public float projectileSpeed=150;


    Vector2 targetVelocity; //this is the velocity we want to have



    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigidbody;

    // Use this for initialization this objects
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ParseInput();

        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 direction = targetVelocity;
            direction.Normalize();


            GameObject instance = Instantiate(projectile, rigidbody.position+(direction*0.5f), new Quaternion());
            Rigidbody2D projrb2d = instance.GetComponent<Rigidbody2D>();

            projrb2d.AddForce(direction*projectileSpeed);
        }
    }


    private void FixedUpdate()
    {
        Vector2 velocity;
        velocity = targetVelocity*Time.deltaTime;

        rigidbody.position += velocity;

    }

    //Basic Movement Controller
    void ParseInput()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");


        //Animator stuff



        targetVelocity = move*speed;
    }
}
