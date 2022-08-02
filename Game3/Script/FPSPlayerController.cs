using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 input;
    CharacterController _controller;
    public float gravity = 9.81f;
    public float moveSpeed = 1;
    public float jumpHeight = 5f;
    public float airControl = 10f;
    public AudioClip jumpSFX;
    Vector3 moveDirection;
    float currentSpeed;
    bool onPlatform = false;
    Vector3 savePosition;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            currentSpeed *= 1.3f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)) {
            currentSpeed = moveSpeed;
        }
        
        if(!LevelManager.isGameOver) {
            if(transform.parent == null) {
                onPlatform = false;
            }
            else {
                onPlatform = true;
            }
            movePlayer();
        }
    }

    void movePlayer(){
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input *= currentSpeed;

        if(_controller.isGrounded) {
            moveDirection = input;
            //jump
            if(Input.GetButton("Jump")) {
                AudioSource.PlayClipAtPoint(jumpSFX, transform.position);
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
            else {
                //ground the object
                moveDirection.y = 0.0f;
            }
        }
        else {
            //mid-air
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, Time.deltaTime * airControl);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        if (onPlatform) {
            if(Input.anyKey) {
                _controller.Move(moveDirection * Time.deltaTime);
            }
        }
        else {
            _controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
