using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleBehavior : MonoBehaviour
{
    public GameObject target;
    private bool displayed = false;
    public string directionMessage;
    void Start()
    {
        if (target == null) {
            target = GameObject.Find("Cube");
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = transform.position - target.transform.position;
        var distanceToPlayer = playerDirection.magnitude;
        if (distanceToPlayer < 3 && !displayed) {
            displayed = true;
            checkTargetDirection();
        }
    }
    private void checkTargetDirection() {
        Vector3 forward = transform.forward;
        Vector3 dir = (target.transform.position - transform.position);
        dir.Normalize();
        float direction = Vector3.Dot(forward, dir);
        if (direction > 0.1) {
            directionMessage = "Cube is in front of me";
        }
        else if (direction < -0.1) {
            directionMessage = "Cube is behind me";
        }
        else {
            directionMessage = "Cube is at right hand side of me";
        }
    }
    
    private void OnGUI() { 
        if (displayed) {
            GUI.Button(new Rect (30, 25, 400, 40), this.name + " :" + directionMessage);
        }
    }
}
