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
        facing = new Vector2(0,1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ParseInput();

        LookAtMouse();



        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 targetForward = transform.rotation * Vector2.right;

            GameObject instance = Instantiate(projectile, rigidbody.position+(targetForward * 0.7f), new Quaternion());
            Rigidbody2D projrb2d = instance.GetComponent<Rigidbody2D>();



            projrb2d.AddForce(targetForward * projectileSpeed);
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

    void LookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
