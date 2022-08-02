using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellBehavior : MonoBehaviour
{
    PlayerHealth playerHealth;
    public int minDamage = 4;
    public int maxDamage = 8;
    GameObject player;
    public AudioClip attackSFX;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        
        transform.LookAt(player.transform);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int spellDamage = Random.Range(minDamage, maxDamage);
            playerHealth.TakeDamage(spellDamage);
            AudioSource.PlayClipAtPoint(attackSFX, Camera.main.transform.position);
        }
    }
}
