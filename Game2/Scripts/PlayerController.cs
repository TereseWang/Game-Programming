using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    float jumpSpeed = 1f;
    float currentSpeed;
    Rigidbody rb;
    public AudioClip jumpSFX;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        if(!LevelManager.isGameOver) {  
            ControlPlayer();
        }
        else {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;         
        }
    }

    void ControlPlayer()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 forceVector = new Vector3(
            moveHorizontal, 0.0f, moveVertical
        );
        rb = GetComponent<Rigidbody>();
        rb.AddForce(forceVector * currentSpeed);
        if (Input.GetButton("Jump")) {
            if(transform.position.y < 1.9f) {
                Vector3 jumpVector = new Vector3(0, jumpSpeed, 0);
                AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position);
                rb.AddForce(jumpVector, ForceMode.Impulse);
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            currentSpeed *= 2;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)) {
            currentSpeed = moveSpeed;
        }
        if(transform.position.y < 0.0f) {
            FindObjectOfType<LevelManager>().LevelLost();
        }
    }
}
