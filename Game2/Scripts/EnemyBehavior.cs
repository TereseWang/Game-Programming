using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    float moveSpeed = 1f;
    public GameObject player;
    public AudioClip damageSFX;
    Rigidbody rb;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }



    void Update()
    {
        if(!LevelManager.isGameOver) {
            RotateEnemy();
            FollowPlayer();
        }
        else {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;  
        }
    }

    void RotateEnemy()
    {
        // apply full rotation the enemy in the forward direction every second
        gameObject.transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
    }

    void FollowPlayer()
    {
        rb = GetComponent<Rigidbody>();
        if (!LevelManager.isGameOver) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(damageSFX, Camera.main.transform.position);
            Destroy(collision.collider);
            Destroy(gameObject);
        }
    }
}
