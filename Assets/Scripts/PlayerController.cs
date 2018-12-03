using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 2f;
    private float maxSpeed;

    public AudioClip shootSound;
    public GameObject projectile;
    public float projectileSpeed=150;
    public float healthGain = 40;
    public float selfDamage = 5;

    Vector2 targetVelocity; //this is the velocity we want to have


    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigidbody;

    [HideInInspector]
    public bool locked=false;
    private GameObject target;

    // Use this for initialization this objects
    private void Awake()
    {
        maxSpeed = speed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        ParseInput();

        LookAtMouse();
    }

	public void fire(){
        SoundManager.PlaySingleAt(shootSound, transform.position);

		Vector2 targetForward = transform.rotation * Vector2.down;

        Vector2 projectilePosition= rigidbody.position + (targetForward * 0.7f);

		GameObject instance = Instantiate(projectile, new Vector3(projectilePosition.x, projectilePosition.y, -0.3f), transform.rotation);
		Rigidbody2D projrb2d = instance.GetComponent<Rigidbody2D>();
        GetComponent<LivingEntity>().LooseHealth(selfDamage);
		projrb2d.AddForce(targetForward * projectileSpeed);
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

		float legAngle = Mathf.Atan2 (move.y, move.x);
		transform.GetChild (0).eulerAngles = new Vector3 (0, 0, legAngle/Mathf.PI*180+90);
		animator.SetFloat ("walkingSpeed", Mathf.Sqrt (move.x * move.x + move.y * move.y));
		if (Input.GetButtonDown ("Fire1"))
			animator.SetTrigger ("shoot");

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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }

    public void Lock(GameObject target)
    {
        speed = 0;
        rigidbody.velocity = new Vector2();
        this.target = target;
    }
    public void Unlock()
    {
        speed = maxSpeed;
        GetComponent<Animator>().SetBool("harvest", false);
    }
    public void finishHarvest(){
        GetComponent<LivingEntity>().GainHealth(healthGain);
        Destroy(target);
        Unlock();
    }
}
