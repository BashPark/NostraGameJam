using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody enemyRB;
    [SerializeField] private Transform playerTransform;
    public LayerMask whatIsPlayer;

    //patroling
    [SerializeField] private GameObject plane;
    [SerializeField] private BoxCollider safeZone;
    [SerializeField] private float pointRadius;
    [SerializeField] private float patrolSpeed;
    private Vector3 targetPoint;

    // Chasing
    [SerializeField] private float chaseSpeed;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, AttackRange;
    public bool playerInSightRange, playerInAttackRange;

    //animations
    [SerializeField] private Animator tigerAnimator;
    [SerializeField] private Animator bearAnimator;
    [SerializeField] private Animator wolfAnimator;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        enemyRB = GetComponent<Rigidbody>();
        setNewPatrolPoint();
       
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange,whatIsPlayer);
        playerInAttackRange= Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) patroling();
        if (playerInSightRange && !playerInAttackRange) chasePlayer();
        if (playerInAttackRange && playerInSightRange) attackPlayer();
         
    }
    private void patroling()
    {
        agent.speed = patrolSpeed;

        agent.SetDestination(targetPoint);


        if(Vector3.Distance(transform.position, targetPoint) < 0.5f)
        {
            setNewPatrolPoint();
        }

        //animations
        // For Tiger
        if (tigerAnimator.gameObject.activeInHierarchy)
        {
            tigerAnimator.SetBool("walk",true);
            tigerAnimator.SetBool("run", false);

        }

        // For bear
        if (bearAnimator.gameObject.activeInHierarchy)
        {
            bearAnimator.SetBool("walk", true);
            bearAnimator.SetBool("run", false);

        }

        // For wolf
        if (wolfAnimator.gameObject.activeInHierarchy)
        {
            wolfAnimator.SetBool("walk", true);
            wolfAnimator.SetBool("run", false);

        }

    }
    private void chasePlayer()
    {
        agent.speed = chaseSpeed;

        agent.SetDestination(playerTransform.position);
        
        
        //animations
        // For Tiger
        if (tigerAnimator.gameObject.activeInHierarchy)
        {
            tigerAnimator.SetBool("walk", false);
            tigerAnimator.SetBool("run", true);

        }

        // For bear
        if (bearAnimator.gameObject.activeInHierarchy)
        {
            bearAnimator.SetBool("walk", false);
            bearAnimator.SetBool("run", true);

        }

        // For wolf
        if (wolfAnimator.gameObject.activeInHierarchy)
        {
            wolfAnimator.SetBool("walk", false);
            wolfAnimator.SetBool("run", true);

        }


    }
    private void attackPlayer()
    {
        // Stop in place and look at player
        agent.SetDestination(playerTransform.position);
        transform.LookAt(playerTransform);
        agent.speed = 0f;
        agent.velocity = Vector3.zero;

        // Handle attack


        // Attack animation

        // For tiger
        if (tigerAnimator.gameObject.activeInHierarchy)
        {
            tigerAnimator.SetTrigger("attack");

        }

        // For bear
        if (bearAnimator.gameObject.activeInHierarchy)
        {
            bearAnimator.SetTrigger("attack");

        }

        // For wolf
        if (wolfAnimator.gameObject.activeInHierarchy)
        {
            wolfAnimator.SetTrigger("attack");

        }


        // Rest Attack timer
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(resetAttack), timeBetweenAttacks);
        }
    }
    private void resetAttack()
    {
        alreadyAttacked=false;

    }

    private void setNewPatrolPoint()
    {
        Vector3 randomPoint;
        do
        {
            randomPoint = getRandomPointOnPlane();
        }while(isInsideSafeZone(randomPoint) && !isNotSafeZone(randomPoint));

        targetPoint = randomPoint;
    }

    private Vector3 getRandomPointOnPlane()
    {
        
        float randomX=Random.Range(transform.position.x-pointRadius, transform.position.x+pointRadius);
        float randomZ=Random.Range(transform.position.z-pointRadius, transform.position.z+pointRadius);

        return new Vector3(randomX,0, randomZ);
    }
    private bool isInsideSafeZone(Vector3 point)
    {
        return safeZone.bounds.Contains(point);
    }
    private bool isNotSafeZone(Vector3 point)
    {
        return plane.GetComponent<MeshRenderer>().bounds.Contains(point);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(targetPoint, 2f);

        if(safeZone != null)
        {
            Gizmos.color= Color.cyan;
            Gizmos.DrawWireCube(safeZone.bounds.center, safeZone.bounds.size);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

    }
}
