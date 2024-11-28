using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;
    public LayerMask whatIsPlayer;

    //patroling
    [SerializeField] private Transform plane;
    [SerializeField] private BoxCollider safeZone;
    [SerializeField] private float pointRadius;
    [SerializeField] private float patrolSpeed;
    private Vector3 targetPoint;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, AttackRange;
    public bool playerInSightRange, playerInAttackRange;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
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
        agent.speed = 5f;
        agent.SetDestination(targetPoint);
        if(Vector3.Distance(transform.position, targetPoint) < 0.5f)
        {
            setNewPatrolPoint();
        }
    }
    private void chasePlayer()
    {
        agent.speed = 5f;
        agent.SetDestination(playerTransform.position);
        

    }
    private void attackPlayer()
    {

    }

    private void setNewPatrolPoint()
    {
        Vector3 randomPoint;
        do
        {
            randomPoint = getRandomPointOnPlane();
        }while(isInsideSafeZone(randomPoint));

        targetPoint = randomPoint;
    }

    private Vector3 getRandomPointOnPlane()
    {
        Vector3 planeCenter = plane.position;
        float randomX=Random.Range(planeCenter.x-pointRadius,planeCenter.x+pointRadius);
        float randomZ=Random.Range(planeCenter.z-pointRadius,planeCenter.z+pointRadius);
        return new Vector3(randomX,planeCenter.y,planeCenter.z);
    }
    private bool isInsideSafeZone(Vector3 point)
    {
        return safeZone.bounds.Contains(point);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPoint, 0.5f);
        if(safeZone != null)
        {
            Gizmos.color= Color.cyan;
            Gizmos.DrawWireCube(safeZone.bounds.center, safeZone.bounds.size);
        }
    }
}
