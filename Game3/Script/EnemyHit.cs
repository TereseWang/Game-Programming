using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    private bool poissoned = false;
    private bool slowed = false;
    private float poissonTime = 0.0f;
    private float slowedTime = 0.0f;
    public GameObject body;
    public Color slowColor;
    public GameObject poissonVFX;
    void Start()
    {
        poissonVFX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var enemyHealth = gameObject.GetComponent<EnemyHealth>();

        var enemyFarAI = gameObject.GetComponent<EnemyFarAttackAI>();
        var enemyAI = gameObject.GetComponent<EnemyAI>();

        if(slowed && slowedTime <= 30) {
            if(this.tag == "CloseAttackEnemy") {
                enemyAI.slowed = true;
            }
            else if(this.tag == "FarAttackEnemy") {
                enemyFarAI.slowed = true;
            }

            slowedTime += 0.05f;
            body.GetComponent<ColorChange>().changeShaderColor(slowColor);
        }
        else {
            slowed = false;
            if(this.tag == "CloseAttackEnemy") {
                enemyAI.slowed = false;
            }
            else if(this.tag == "FarAttackEnemy") {
                enemyFarAI.slowed = false;
            }
            slowedTime = 0f;
            body.GetComponent<ColorChange>().changeShaderColor(Color.white);
        }
        if(poissoned && poissonTime <= 15) {
            poissonVFX.SetActive(true);
            enemyHealth.TakeDamage(0.1f);
            poissonTime += 0.1f;
        }
        else {
            poissonVFX.SetActive(false);
            poissoned = false;
            poissonTime = 0f;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("PoissonProjectile")) {
            Destroy(collision.gameObject, 0.5f);
            poissoned = true;
            var enemyHealth = gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(2);
        }
        if(collision.gameObject.CompareTag("IceProjectile")) {
            Destroy(collision.gameObject, 0.5f);
            slowed = true;
            var enemyHealth = gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(5);
        }
        if(collision.gameObject.CompareTag("DamageProjectile")) {
            Destroy(collision.gameObject, 0.5f);
            var enemyHealth = gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(8);
        }
    }
}
