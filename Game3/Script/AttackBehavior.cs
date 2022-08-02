using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public int minDamage = 3;
    public int maxDamage = 5;
    public AudioClip attackSFX;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) {
        int damageAmount = Random.Range(minDamage, maxDamage);
        if(other.CompareTag("Player")) {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
            AudioSource.PlayClipAtPoint(attackSFX, Camera.main.transform.position);
        }
    }
}
