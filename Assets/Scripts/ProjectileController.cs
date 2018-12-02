using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float damage = 10;
    public float lifetime = 50f;
    public int maxBounceCounter=4;
	public float power=100f;

    private int bounceCounter;
	public GameObject effect;

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

    void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.layer == 9) {
			collision.gameObject.layer = 8;
			collision.gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.8f, 0.3f, 0.3f);
		}
		LivingEntity le = collision.gameObject.GetComponent<LivingEntity> ();
		if (le != null) {
			le.TakeDamage (this.damage);
			Vector2 force = (le.transform.position - transform.position).normalized * power;
			le.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (force.x, force.y), ForceMode2D.Impulse);
            Destroy(gameObject);
        }
		Vector2 normal = collision.contacts [0].normal;
		float angle = Mathf.Atan2 (normal.y, normal.x);
		GameObject.Instantiate (effect, transform.position, Quaternion.Euler (new Vector3 (0, 0, angle/Mathf.PI*180-90)));

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
	void OnCollisionExit2D(Collision2D collision)
	{
		Vector2 temp = GetComponent<Rigidbody2D> ().velocity;
		float angle = Mathf.Atan2 (temp.y, temp.x);
		transform.eulerAngles = new Vector3 (0, 0, angle/Mathf.PI*180+90);
	}
}
