using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleMove : MonoBehaviour
{
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 1)
        {
            transform.Translate(0, 2 * Time.deltaTime, 0);
            if (transform.position.y > 13)
            {
                direction = 0;
            }
        }
        else
        {
            transform.Translate(0, -2 * Time.deltaTime, 0);
            if (transform.position.y < 4)
            {
                direction = 1;
            }
        }
    }
}
