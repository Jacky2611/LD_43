using Pathfinding;
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

    //used to determine wether or not the player is standing in the attack area of this enemy
    public bool inAttackArea;
    public float damage;
	public float attackForce;

    //A* Seeker component, used for pathfinding
    private Seeker seeker;
    public Path path;
    public float movementSpeed = 1.8f;
    private int currentWaypoint = 0;
    public float nextWaypointDistance = 2; //How close we must be to a waypoint before we can go to the next
    //recalculate path regularly
    public float repathRate = 0.8f;
    private float lastRepath = float.NegativeInfinity;

    public MeleeEnemyState state;
    public Vector2 direction;
    public float playerDetectionRange = 20;
    public float attackRange = 1.0f;
    private float playerDistance;

    void Awake()
    {
        direction = new Vector2(1, 0);
        animator = gameObject.GetComponent<Animator>();
        seeker = gameObject.GetComponent<Seeker>();
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
                FollowPath();
                break;
            case MeleeEnemyState.ATTACKING:
                direction = player.transform.position - this.gameObject.transform.position;
                Rotate();
                break;
        }


    }

    //Called whenever an A* path for this go is done
    public void OnPathComplete(Path p)
    {
        // Path pooling. To avoid unnecessary allocations paths are reference counted.
        // Calling Claim will increase the reference count by 1 and Release will reduce
        // it by one, when it reaches zero the path will be pooled and then it may be used
        // by other scripts. The ABPath.Construct and Seeker.StartPath methods will
        // take a path from the pool if possible. See also the documentation page about path pooling.
        p.Claim(this);
        if (!p.error)
        {
            //if (path != null) path.Release(this);
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        } else
        {
            p.Release(this);
        }
    }

    /**
     * Follows a generated path an creates new paths every x seconds.
    */
    public void FollowPath()
    {
        //repath from time to time
        if (Time.time > lastRepath + repathRate && seeker.IsDone())
        {
            //Debug.Log("REPATHING");

            lastRepath = Time.time;

            // Start a new path to the targetPosition, call the the OnPathComplete function
            // when the path has been calculated (which may take a few frames depending on the complexity)
            seeker.StartPath(transform.position, player.transform.position, OnPathComplete);
        }


        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        bool reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance )
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {


                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                } 
            }
            else
            {
                break;
            }
        }

        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        Vector3 velocity = dir * movementSpeed * speedFactor;
        


        // If you are writing a 2D game you may want to remove the CharacterController and instead use e.g transform.Translate
        transform.position += velocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.4f);
    }

    void Rotate()
    {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+90));
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

        // Start a new path to the targetPosition, call the the OnPathComplete function
        // when the path has been calculated (which may take a few frames depending on the complexity)
        path = null;
        seeker.StartPath(transform.position, player.transform.position, OnPathComplete);
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

    public void DealDamage()
    {
        if (inAttackArea)
        {
            player.GetComponent<LivingEntity>().TakeDamage(damage);
			Vector3 push = (player.transform.position - transform.position).normalized * attackForce;
			player.GetComponent<Rigidbody2D> ().AddForce (new Vector2(push.x,push.y),ForceMode2D.Impulse);
        }
    }

    public enum MeleeEnemyState
    {
        IDLE, CHASING, ATTACKING
    }
}
