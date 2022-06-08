using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class InkyBehavior : MonoBehaviour
{
    public GameObject[] points;
    public GameObject player;
    public GameObject home;
    private int destPoint = 0;
    public float MaxDist = 0f;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (Door.safe == true)
        {
            agent.destination = home.transform.position;
        }
        else
        {
            if (dist <= MaxDist)
            {
                agent.destination = player.transform.position;
            }

            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GoToNextPoint();
            }
        }
        
    }

    void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].transform.position;
        destPoint = (destPoint + 1) % points.Length;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            SceneManager.LoadScene("MazeTest");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerShot")
        {
            DestroyShot projectile = other.gameObject.GetComponent<DestroyShot>();
            StartCoroutine(Stun(projectile.charge));
            Destroy(other.gameObject);
        }
    }

    IEnumerator Stun(float charge)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(Mathf.Ceil(charge / 5));
        agent.isStopped = false;
    }
}