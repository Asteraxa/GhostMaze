using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ClydeBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject home;
    public float maxDist = 50f;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
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
            if (dist <= maxDist)
            {
                StartCoroutine(GoHome());
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

    IEnumerator GoHome()
    {
        agent.destination = home.transform.position;
        yield return new WaitForSeconds(5f);
        agent.destination = player.transform.position;
    }
}