using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAWhile : MonoBehaviour
{
    // Start is called before the first frame update
    public float destroyDelay = 2f;
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
