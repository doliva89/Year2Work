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

    

    public bool wandering = true;
    public bool chasing = false;

    public float wanderingSpeed = 0.5f;
    public float wanderingAnimSpeed = 2.5f;
    public float wanderRange = 5.0f;
    public float chaseSpeed = 2.0f;
    public float chaseAnimSpeed = 5.0f;

    

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
        if (wandering != false && !IsInvoking("Wander"))
        {
            InvokeRepeating("Wander", 1f, 5f);
        }
        if(chasing)
        {
            ChasePlayer();
        }
    }

    void Wander()
    {
        Vector3 destination = startPosition + new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));
        NewDestination(destination);
        agent.speed = wanderingSpeed;
        animator.SetBool("chasing", false);
    }

    public void NewDestination(Vector3 targetPoint)
    {
        agent.SetDestination(targetPoint);
    }

    void ChasePlayer()
    {
        NewDestination(player.transform.position);
        agent.speed = chaseSpeed;
        animator.SetBool("chasing", true);
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player")
        {
            wandering = false;
            chasing = true;
            CancelInvoke();
        }
    }

    void OnTriggerExit(Collider player)
    {
        if(player.tag == "Player")
        {
            wandering = true;
            chasing = false;
        }
    }
}
