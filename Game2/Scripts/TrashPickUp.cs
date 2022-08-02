using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPickUp : MonoBehaviour
{
    public static int trashPickupCount = 0;
    public int scoreValue = -1;
    public AudioClip pickupSFX;
    void Start()
    {
        trashPickupCount = trashPickupCount + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            trashPickupCount--;
            GetComponent<Animator>().SetTrigger("trashDestroyed");
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
            LevelManager.score += scoreValue;
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnDestroy() {
        if (LevelManager.score < 0) {
            FindObjectOfType<LevelManager>().LevelLost();
        }
    }
}
