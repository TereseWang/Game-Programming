using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBlock : MonoBehaviour
{
    public float colorChangeSpeed = 2f;
    public Color startColor;
    public Color endColor;    
    private Renderer cubeRenderer;
    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Sin(Time.time *  colorChangeSpeed) + 1;
        t /= 2;
        cubeRenderer.material.color = Color.Lerp(startColor, endColor, t);
    }
    private void OnCollisionEnter(Collision other) {
        GameObject otherObject = other.gameObject;
        Vector3 dir = (otherObject.transform.position - transform.position);
        dir.Normalize();
        Rigidbody rb = otherObject.GetComponent<Rigidbody>();

        if (otherObject.CompareTag("Enemy") ) {
            rb.AddForce(dir * 30);
        }
        else if(otherObject.CompareTag("Player")) {
            rb.AddForce(dir * 200);
        }
    }
}


