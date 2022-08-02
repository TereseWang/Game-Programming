using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject cratePieces;
    public float explosionForce = 100;
    public float explosionRadius = 5;
    public GameObject lootObject;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("ReductoProjectile")) {
            Transform currentCrate = gameObject.transform;

            GameObject pieces = Instantiate(
                cratePieces, currentCrate.position, currentCrate.rotation
            );
            Instantiate(lootObject, transform.position, transform.rotation);
        
            Rigidbody[] rbPieces = pieces.GetComponentsInChildren<Rigidbody>();
            foreach(Rigidbody rb in rbPieces) {
                rb.AddExplosionForce(explosionForce, currentCrate.position, explosionRadius);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
