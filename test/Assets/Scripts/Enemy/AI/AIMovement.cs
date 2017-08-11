using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.Animations;

public class AIMovement : MonoBehaviour {

    private NavMeshAgent agent;
    private Vector3 startPosition;
    private GameObject player;
    private Animator animator;


    //Values that can be changed for when the enemy is wandering around
    public float wanderingSpeed = 0.5f;
    public float wanderingAnimSpeed = 2.5f;
    public float wanderRange = 5.0f;

    //Values that can be changed for the attacking side of enemy
    public float chaseSpeed = 5.0f;
    public float chaseAnimSpeed = 5.0f;
    public float rollSpeed = 3.0f;

    //Values of the players position and if the player is within certain ranges for different phases
    public float playerDistance;
    public float inRangeChase = 5.0f;
    public float inRangeRoll = 2.5f;
    public float outRangeWander = 10.0f;

    //Wander repeating parameters
    public float wanderRepeatTime = 1.0f;
    public float wanderRepeatRate = 5.0f;

    // Use this for initialization
    void Awake ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = wanderingSpeed;
        startPosition = this.transform.position;

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (playerDistance >= outRangeWander && !IsInvoking("Wander") && !IsInvoking("RollAttack"))
        {
            //  print("wandering");
            //animator.SetBool("chasing", false);
            animator.SetBool("isRolling", false);
            InvokeRepeating("Wander", wanderRepeatTime, wanderRepeatRate);
        }
        if(playerDistance <= inRangeChase && playerDistance >= inRangeRoll && !IsInvoking("RollAttack"))
        {
            //  print("chasing range");

            //animator.SetBool("chasing", true);
            animator.SetBool("isRolling", false);


            ChasePlayer();
        }

        if (playerDistance <= inRangeRoll && !IsInvoking("RollAttack"))
        {
            animator.SetBool("isRolling", true);
            Invoke("RollAttack", playerDistance/4);
            print(playerDistance / 4);
            StartCoroutine(Wait(4));
        }
    }


    //Functions
    void Wander()
    {
        Vector3 destination = startPosition + new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));
        NewDestination(destination);
        agent.speed = wanderingSpeed;
    }

    public void NewDestination(Vector3 targetPoint)
    {
        agent.SetDestination(targetPoint);
    }

    void ChasePlayer()
    {
        CancelInvoke();
        agent.speed = chaseSpeed;
        NewDestination(player.transform.position);
        
    }

    void RollAttack()
    {
        NewDestination(player.transform.position);
    }

    IEnumerator Wait(float waitTime)
    {
        print("time " + Time.time);
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("isRolling", false);
        print("stop rolling");
        print("time " + Time.time);
    }




    //Collider used to tell the AI if its in range of the player and to switch states
    //void OnTriggerEnter(Collider player)
    //{
    //    if(player.tag == "Player")
    //    {
    //        wandering = false;
    //        chasing = true;
    //        CancelInvoke();
    //    }
    //}

    //void OnTriggerExit(Collider player)
    //{
    //    if(player.tag == "Player")
    //    {
    //        wandering = true;
    //        chasing = false;
    //    }
    //}
}
