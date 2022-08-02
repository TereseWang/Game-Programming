using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int startingHealth = 100;
    int currentHealth;
    public AudioClip deadSFX;
    public Slider healthSlider;
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageAmount) {
        if(currentHealth > 0) {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        if(currentHealth <= 0) {
            PlayerDies();
        }
    }
    public void TakeHealth(int healthAmount) {
        if(currentHealth < startingHealth) {
            currentHealth += healthAmount;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100);
        }
    }
    void PlayerDies() {
        FindObjectOfType<LevelManager>().LevelLost();
    }
}
