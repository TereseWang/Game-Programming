using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject iceProjectilePrefab;
    public GameObject damageProjectilePrefab;
    public GameObject poissonProjectilePrefab;
    public float projectileSpeed = 100f;
    public Image reticalImage;
    Color originalReticleColor;
    GameObject currentProjectilePrefab;
    public GameObject breakableProjectile;
    GameObject lastProjectile;
    void Start()
    {
        originalReticleColor = reticalImage.color;
        currentProjectilePrefab = damageProjectilePrefab;
        lastProjectile = damageProjectilePrefab;
    }

    void Update()
    {
        if(!TutorialWindow.isGamePaused) {
            if(!LevelManager.isGameOver) {
                changeProjectilePrefab();
                if(Input.GetButtonDown("Fire1")) {
                    Shoot();
                }
            }
        }
    }

    private void FixedUpdate() {
        reticalEffect();
    }

    void changeProjectilePrefab() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            originalReticleColor = Color.white;
            currentProjectilePrefab = damageProjectilePrefab;
            lastProjectile = damageProjectilePrefab;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            originalReticleColor = Color.green;
            currentProjectilePrefab = poissonProjectilePrefab;
            lastProjectile = poissonProjectilePrefab;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3)) {
            originalReticleColor = Color.blue;
            currentProjectilePrefab = iceProjectilePrefab;
            lastProjectile = iceProjectilePrefab;
        }
    }

    void Shoot() {
        GameObject projectile = Instantiate(currentProjectilePrefab, 
                                                transform.position + transform.forward,
                                                transform.rotation);
        var _rb = projectile.GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
    }
    
    void reticalEffect() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) {
            if(hit.collider.CompareTag("Reducto")) {
                currentProjectilePrefab = breakableProjectile;
            }
            else {
                currentProjectilePrefab = lastProjectile;
            }
            if(hit.collider.CompareTag("CloseAttackEnemy") || hit.collider.CompareTag("FarAttackEnemy")) {
                reticalImage.color = Color.Lerp(reticalImage.color, 
                                        originalReticleColor, Time.deltaTime * 2);
                reticalImage.transform.localScale = Vector3.Lerp(reticalImage.transform.localScale, 
                                    new Vector3(0.5f, 0.5f, 1), Time.deltaTime * 2);
            }
            else {
                reticalImage.color = Color.Lerp(reticalImage.color, 
                                        originalReticleColor, Time.deltaTime * 2);
                reticalImage.transform.localScale = Vector3.Lerp(reticalImage.transform.localScale, 
                                    new Vector3(1, 1, 1), Time.deltaTime * 2);
            }
        }
        else {
            reticalImage.color = Color.Lerp(reticalImage.color, 
                                        originalReticleColor, Time.deltaTime * 2);
            reticalImage.transform.localScale = Vector3.Lerp(reticalImage.transform.localScale, 
                                    new Vector3(1, 1, 1), Time.deltaTime * 2);  
        }
    }
}
