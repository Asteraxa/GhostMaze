using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PinkyBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject home;
    public float maxDist;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Vector3 target = player.transform.position + (50 * player.transform.forward);
        agent.destination = target;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.transform.position + (50 * player.transform.forward);
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (Door.safe == true)
        {
            agent.destination = home.transform.position;
        }
        else
        {
            if (dist <= maxDist)
            {
                agent.destination = player.transform.position;
            }
            else
            {
                agent.destination = target;
            }
        }  
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