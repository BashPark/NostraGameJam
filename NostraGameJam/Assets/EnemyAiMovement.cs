using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.FilePathAttribute;

public class EnemyAiMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
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

    // Knockback
    [SerializeField] private float knockbackForce = 5f; // Adjust the force as needed


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        plane = GameObject.FindGameObjectWithTag("WalkableArea");
        safeZone = GameObject.FindGameObjectWithTag("Safezone").GetComponent<BoxCollider>();

        targetPoint = new Vector3(20, 0, 20);
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) patroling();
        else if (playerInSightRange && !playerInAttackRange) chasePlayer();
        else if (playerInAttackRange && playerInSightRange) attackPlayer();


    }

    private void patroling()
    {
        agent.speed = patrolSpeed;

        if(agent.isOnNavMesh)
        {
       
            agent.SetDestination(targetPoint);

        }


        if (Vector3.Distance(transform.position, targetPoint) < 0.5f)
        {
            setNewPatrolPoint();
        }

        //animations
        // For Tiger
        if (tigerAnimator.gameObject.activeInHierarchy)
        {
            tigerAnimator.SetBool("walk", true);
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

        // Get the player's position
        Vector3 targetPosition = playerTransform.position;

        // Keep the current object's Y position to ignore vertical rotation
        targetPosition.y = transform.position.y;

        // Rotate the object to look at the adjusted position
        transform.LookAt(targetPosition);

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
        alreadyAttacked = false;

    }

    private void setNewPatrolPoint()
    {
        Vector3 randomPoint;
        do
        {
            randomPoint = getRandomPointOnPlane();
        } while (isInsideSafeZone(randomPoint) && !isNotSafeZone(randomPoint));

        targetPoint = randomPoint;
    }

    private Vector3 getRandomPointOnPlane()
    {
        
        float randomX = Random.Range(plane.transform.position.x - pointRadius, plane.transform.position.x + pointRadius);
        float randomZ = Random.Range(plane.transform.position.z - pointRadius, plane.transform.position.z + pointRadius);

        return new Vector3(randomX, 0, randomZ);
    }
    private bool isInsideSafeZone(Vector3 point)
    {
        return safeZone.bounds.Contains(point);
    }
    private bool isNotSafeZone(Vector3 point)
    {
        return plane.GetComponent<MeshRenderer>().bounds.Contains(point);
    }

    // Call this function when the enemy is hit
    public void ApplyKnockback()
    {
        // Calculate the knockback direction (opposite of forward)
        Vector3 knockbackDirection = -transform.forward.normalized;

        // Calculate the target position for the knockback
        Vector3 knockbackTarget = transform.position + knockbackDirection * knockbackForce;

        // Instantly move the enemy to the knockback target
        transform.position = Vector3.MoveTowards(transform.position, knockbackTarget, 200 * Time.deltaTime);




    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(targetPoint, 2f);

        if (safeZone != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(safeZone.bounds.center, safeZone.bounds.size);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

    }
}
