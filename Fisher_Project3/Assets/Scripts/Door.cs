using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static bool safe;

    private bool triggered;

    private void Start()
    {
        safe = false;
        triggered = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            if (triggered == false)
            {
                if (safe == false)
                {
                    safe = true;
                    Debug.Log(safe);
                }
                else
                {
                    safe = false;
                    Debug.Log(safe);
                }
                triggered = true;
            }  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }
}
