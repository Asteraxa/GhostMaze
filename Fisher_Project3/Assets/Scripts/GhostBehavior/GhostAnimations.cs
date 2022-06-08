using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAnimations : MonoBehaviour
{

    public GameObject player;
    Animator anim;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        checkMoving();
        checkStunned();
    }

    void checkMoving()
    {
        if (agent.velocity != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void checkStunned()
    {
        if (agent.isStopped)
        {
            anim.SetBool("isStunned", true);
        }
        else
        {
            anim.SetBool("isStunned", false);
        }
    }
}
