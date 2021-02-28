using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindObjectOfType<PlayerController>();
        //agent = GetComponent<NavMeshAgent>();

        agent.Warp(gameObject.transform.position + Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) < 10)
        {
            agent.SetDestination(player.transform.position);
            animator.SetBool("Run Forward", true);
        }
        else
        {
            animator.SetBool("Run Forward", false);
        }
    }
}
