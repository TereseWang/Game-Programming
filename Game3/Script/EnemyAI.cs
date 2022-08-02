using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum FSMStates {
        Idle, 
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public FSMStates currentState;

    CharacterController _controller;
    GameObject[] wanderPoints;
    GameObject[] visitedPoints;

    Animator anim;
    Vector3 nextDestination;
    public float attackRate = 2;
    float elapsedTime = 0;

    int currentDestinationIndex;

    public float chaseDistance = 10;
    public GameObject player;
    public bool slowed = false;
    public float attackDistance = 5;
    public float enemySpeed = 1f;
    float currentSpeed;
    float distanceToPlayer;
    public Transform enemyEyes;

    NavMeshAgent agent;
    public float fieldOfView = 45f;
    

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        currentDestinationIndex = Random.Range(0, wanderPoints.Length);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = FSMStates.Patrol;
        agent = GetComponent<NavMeshAgent>();
        FindNextPoint();
        currentSpeed = enemySpeed;
        agent.speed = currentSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!LevelManager.isGameOver) {
            statusUpdate();
        }
        else {
            anim.SetInteger("animState", 0);
            currentState = FSMStates.Idle;
            FaceTarget(nextDestination);
        }
    }

    void statusUpdate() {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(slowed) {
            currentSpeed = currentSpeed * 0.5f;
            agent.speed = currentSpeed;
        }
        else {
            if(currentState == FSMStates.Chase) {
                currentSpeed = enemySpeed * 1.2f;
                agent.speed = currentSpeed;
            }
            else {
                currentSpeed = enemySpeed;
                agent.speed = currentSpeed;
            }
        }
        switch(currentState) {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }
        elapsedTime += Time.deltaTime;
    }
    void FaceTarget(Vector3 target) {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
    void UpdateChaseState() {
        nextDestination = player.transform.position;
        if(slowed) {
            anim.SetInteger("animState", 4);
        }
        else {
            anim.SetInteger("animState", 5);
        }
        if(distanceToPlayer <= attackDistance) {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance) {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdatePatrolState() {
        anim.SetInteger("animState", 1);
        agent.stoppingDistance = 0;
        if(Vector3.Distance(transform.position, nextDestination) < 1) {
            FindNextPoint();
        }
        else if(distanceToPlayer <= chaseDistance && IsPlayerInClearFOV()) {
            currentState = FSMStates.Chase;
        }
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void FindNextPoint() {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState() {
        nextDestination = player.transform.position;
        agent.stoppingDistance = attackDistance * 0.8f;
        if(distanceToPlayer <= attackDistance){
            currentState = FSMStates.Attack;
        }
        else if(distanceToPlayer > attackDistance /*&& distanceToPlayer <= chaseDistanc*/) {
            currentState = FSMStates.Chase;
        }
        else if(distanceToPlayer > chaseDistance) {
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);
        anim.SetInteger("animState", 2);
    }

    void UpdateDeadState() {
        currentSpeed = 0.0f;
        anim.SetInteger("animState", 3);
    }

    public void updateDeadState() {
        currentState = FSMStates.Dead;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;
        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.blue);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
    }

    bool IsPlayerInClearFOV() {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;
        if(Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView) {
            if(Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance)) {
                if(hit.collider.CompareTag("Player")) {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}
