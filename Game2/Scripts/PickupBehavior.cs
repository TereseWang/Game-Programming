using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public int scoreValue = 1;
    public AudioClip pickupSFX;
    public AudioClip bonusSFX;
    int levelDuration;
    int currentScoreValue;
    void Start()
    {
        LevelManager.pickupCount = LevelManager.pickupCount + 1;
        levelDuration = FindObjectOfType<LevelManager>().levelDuration;
        currentScoreValue = scoreValue;
    }
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            LevelManager.pickupCount--;
            GetComponent<Animator>().SetTrigger("pickupDestroyed");
            if(FindObjectOfType<LevelManager>().countDown >= Mathf.Floor(levelDuration /2)) {
                AudioSource.PlayClipAtPoint(bonusSFX, Camera.main.transform.position);
                currentScoreValue *= 2;
            }
            else {
                AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
                currentScoreValue = scoreValue;
            }
            LevelManager.score += currentScoreValue;
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnDestroy() {
        if (LevelManager.pickupCount<=0) {
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }
}
