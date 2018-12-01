using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 2f;


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
