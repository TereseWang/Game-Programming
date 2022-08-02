using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100;
    Transform playerBody;
    float pitch = 0;
    //float yaw = 0;
    // Start is called before the first frame update
    void Start()
    {

        playerBody = transform.parent.transform;
    }
    private void FixedUpdate() {
        if (!TutorialWindow.isGamePaused) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //yaw +=  moveX;
        playerBody.Rotate(Vector3.up * moveX);

        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, -75, 75);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
