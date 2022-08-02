using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFarAttackAI : MonoBehaviour
{
   public enum FAEStates {
        Idle, 
        //Patrol,
        Chase,
        Attack,
        Dead,
        Frozen
    }

    public FAEStates currentState;

    CharacterController _controller;
    GameObject[] wanderPoints;
    public GameObject spellProjectile;
    public GameObject wandTip;

    Animator anim;
    Vector3 nextDestination;
    public float shootRate = 2;
    float elapsedTime = 0;

    //int currentDestinationIndex = 0;

    //public float chaseDistance = 10;
    public GameObject player;
    public bool slowed = false;
    public float attackDistance = 5;
    public float enemySpeed = 1f;
    float currentSpeed;
    float distanceToPlayer;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        //wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = FAEStates.Chase;
        currentSpeed = enemySpeed;
        wandTip = GameObject.FindGameObjectWithTag("WandTip");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = currentSpeed;
        //Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver) {
            statusUpdate();
        }
        else {
            anim.SetInteger("animState", 0);
            currentState = FAEStates.Idle;
            FaceTarget(nextDestination);
        }
    }
    void statusUpdate() {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        currentSpeed = enemySpeed;
        agent.speed = currentSpeed;

        switch(currentState) {
            case FAEStates.Frozen:
                UpdateFrozenState();
                break;
            case FAEStates.Chase:
                UpdateChaseState();
                break;
            case FAEStates.Attack:
                UpdateAttackState();
                break;
            case FAEStates.Dead:
                UpdateDeadState();
                break;
        }
        elapsedTime += Time.deltaTime;
    }

    void UpdateFrozenState() {
        nextDestination = player.transform.position;
        if(slowed) {
            anim.SetInteger("animState", 4);
        }
        else {
            if(distanceToPlayer <= attackDistance) {
                currentState = FAEStates.Attack;
            }

            else if(distanceToPlayer > attackDistance) {
                currentState = FAEStates.Chase;
            }
        }
        FaceTarget(nextDestination);
    }

    void FaceTarget(Vector3 target) {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
    void UpdateChaseState() {
        //print("Chasing");
        nextDestination = player.transform.position;
        if(slowed) {
            currentState = FAEStates.Frozen;
        }
        if(distanceToPlayer <= attackDistance) {
            currentState = FAEStates.Attack;
        }
        /*
        else if(distanceToPlayer > chaseDistance) {
            currentState = FAEStates.Patrol;
        }
        */
        FaceTarget(nextDestination);
        anim.SetInteger("animState", 3);
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState() {
        //print("attack");
        agent.stoppingDistance = attackDistance;
        if(slowed) {
            currentState = FAEStates.Frozen;
        }
        else {
            nextDestination = player.transform.position;
            if(distanceToPlayer <= attackDistance){
                currentState = FAEStates.Attack;
            }
            else if(distanceToPlayer > attackDistance /*&& distanceToPlayer <= chaseDistanc*/) {
                currentState = FAEStates.Chase;
            }

            FaceTarget(nextDestination);
        
            int picker = Random.Range(0, 1);
            if(picker == 0) {
                anim.SetInteger("animState", 1);               
            }
            else {
                anim.SetInteger("animState", 2);
            }

            EnemySpellCast();
        }

    }

    void EnemySpellCast() {
        if(elapsedTime >= shootRate) {
            var animeDuration = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("SpellCasting", animeDuration);
            elapsedTime = 0;
        }
    }


    void SpellCasting() {
        if(wandTip != null) {   
            Instantiate(spellProjectile, wandTip.transform.position, wandTip.transform.rotation);
        }
    }

    void UpdateDeadState() {
        currentSpeed = 0;
        anim.SetInteger("animState", 5);
    }

    public void updateDeadState() {
        currentState = FAEStates.Dead;
    }

    /*
    void FindNextPoint() {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    }
    */
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

    /*
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        */
    }
}
