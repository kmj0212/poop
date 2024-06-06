using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(0, -0.01f, 0);

        if (transform.position.y < -3.5f)
        {
            Destroy(gameObject);
        }
    }
}
