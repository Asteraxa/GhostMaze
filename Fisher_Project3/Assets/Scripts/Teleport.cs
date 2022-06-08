using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    CharacterController cc;

    public GameObject player;
    public GameObject east;
    public GameObject west;
    private Vector3 eastPort = Vector3.zero;
    private Vector3 westPort = Vector3.zero;

    void Start()
    {
        cc = player.GetComponent<CharacterController>();
        eastPort = east.transform.position;
        westPort = west.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            if (player.transform.position.x > 0)
            {
                cc.enabled = false;
                player.transform.position = westPort;
                cc.enabled = true;
            }
            else
            {
                cc.enabled = false;
                player.transform.position = eastPort;
                cc.enabled = true;
            }
        }
    }
}
