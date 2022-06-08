using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShot : MonoBehaviour
{
    public float lifespan = 2f;
    public float charge;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifespan);
    }
}
