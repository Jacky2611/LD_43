using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MeleeEnemyController : MonoBehaviour
{

    //this enemies target
    private GameObject player;

    //this is used to transition between diferent AI states
    private Animator animator;

    public MeleeEnemyState state;
    public Vector2 direction;
    public float playerDetectionRange = 20;
    public float attackRange = 1.5f;
    private float playerDistance;

    void Awake()
    {
        direction = new Vector2(1, 0);
        animator = gameObject.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        this.player= GameObject.FindGameObjectsWithTag("Player")[0];
        if (player == null)
            Debug.LogError("unable to find player");
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        // player.transform.position.D   //this.gameObject.transform.

        //update paramaters for Ai Controller
        animator.SetBool("playerInRange", playerDistance < playerDetectionRange);
        animator.SetBool("playerInAttackRange", playerDistance < attackRange);
        animator.SetFloat("playerDistance", playerDistance);
    }

    void FixedUpdate()
    {
        switch (this.state)
        {
            case MeleeEnemyState.IDLE:
                break;
            case MeleeEnemyState.CHASING:
                direction = player.transform.position - this.gameObject.transform.position;
                Rotate();
                break;
            case MeleeEnemyState.ATTACKING:
                direction = player.transform.position - this.gameObject.transform.position;
                Rotate();
                break;
        }


    }

    void Rotate()
    {
        direction = direction.normalized;
        float Angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,Angel-90));
    }


    public void StartIdle()
    {
        this.state = MeleeEnemyState.IDLE;
    }

    public void StopIdle()
    {

    }

    public void StartChasing()
    {
        this.state = MeleeEnemyState.CHASING;
    }

    public void StopChasing()
    {

    }

    public void StartAttacking()
    {
        this.state = MeleeEnemyState.ATTACKING;
    }

    public void StopAttacking()
    {

    }

    public enum MeleeEnemyState
    {
        IDLE, CHASING, ATTACKING
    }
}
