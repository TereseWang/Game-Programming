using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    void Start()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        offset = transform.position - player.position;
    }

    void Update()
    {
        if(!LevelManager.isGameOver) {
            transform.position = player.position + offset;
        }
    }
}
