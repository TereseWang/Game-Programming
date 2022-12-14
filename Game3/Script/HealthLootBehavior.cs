using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLootBehavior : MonoBehaviour
{
    public int healthAmount = 5;
    public AudioClip lootSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < Random.Range(2.0f, 3.5f)) {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(lootSFX, transform.position);
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeHealth(healthAmount);
            Destroy(gameObject, 0.5f);
        }
    }
}
