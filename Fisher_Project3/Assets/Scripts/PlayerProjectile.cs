using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    public float charge;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Hello());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator Hello()
    {
        Debug.Log("Hello");
        yield return new WaitForSeconds(Mathf.Floor(charge/5));
        Debug.Log("World");
    }
}
