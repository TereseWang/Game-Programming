using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    void Start(){ 
    }

    void Update(){    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) {
            FindObjectOfType<LevelManager>().LevelLost();
            Destroy(gameObject);
        }
    }
}
