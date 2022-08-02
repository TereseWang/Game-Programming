using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCrate : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3;
    public float distance = 5;
    Vector3 startPos; 
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.x = startPos.x + Mathf.Sin(Time.time * speed) * distance;
        transform.position = newPos;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            other.transform.SetParent(transform, true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            other.transform.SetParent(null);
        }
    }
}
