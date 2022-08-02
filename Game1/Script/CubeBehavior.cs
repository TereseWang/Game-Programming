using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    private GameObject[] players;
    private float enemySpeed = 3f;
    private bool moveTowardPlayer;
    private GameObject currentPlayer;
    public int minDistance = 1;
    void Start()
    {
        learnPlayer();
    }

    void learnPlayer() {
        players = GameObject.FindGameObjectsWithTag("Player");
        int playerIndex = Random.Range(0, players.Length);
        currentPlayer = players[playerIndex];
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            moveTowardPlayer = true;
        }
        if (moveTowardPlayer) {
            FollowPlayer();
        }
    }
    void FollowPlayer() {
        float t = enemySpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(
            transform.position, 
            currentPlayer.transform.position, t);

        float distanceToPlayer = Vector3.Distance(
            transform.position, 
            currentPlayer.transform.position);
    
        if (distanceToPlayer <= minDistance) {
            destroyPlayer();
        }
    }
    void destroyPlayer() {
        if (players.Length > 1) {
            currentPlayer.SetActive(false);
            learnPlayer();
        }
        else {
            currentPlayer.SetActive(false);
            moveTowardPlayer = false;
        }
    }
}
