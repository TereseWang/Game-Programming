using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 50;
    public Slider healthSlider1;
    public GameObject deathEffect;
    public GameObject breakable;
    public Slider healthSlider2;
    float currentHealth;
    bool isDead = false;
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider1.maxValue = startingHealth;
        healthSlider1.value = currentHealth; 
        healthSlider2.maxValue = startingHealth;
        healthSlider2.value = currentHealth; 
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void TakeDamage(float damageAmount) {
        if(Mathf.Floor(currentHealth) > 0.0f) {
            currentHealth -= damageAmount;
        }
        if(Mathf.Floor(currentHealth) <= 0.0f && !isDead) {
            isDead = true;
            var enemyAI = gameObject.GetComponent<EnemyAI>();
            var enemyFarAI = gameObject.GetComponent<EnemyFarAttackAI>();
            if (this.tag == "CloseAttackEnemy") {
                enemyAI.updateDeadState();
            }
            else if(this.tag == "FarAttackEnemy") {
                enemyFarAI.updateDeadState();
            }
            //gameObject.SetActive(false);
            Instantiate(deathEffect, transform.position, transform.rotation);
            LevelManager.score += 1;
            Instantiate(breakable, transform.position, transform.rotation);
            Destroy(gameObject, 1.5f);
        }
        healthSlider1.value = currentHealth;
        healthSlider2.value = currentHealth;
    }
}
