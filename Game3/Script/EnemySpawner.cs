using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    GameObject currentEnemyPrefab;
    public float spawnTime = 10;
    public int numFarAttackEnemy;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", 0, spawnTime);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemies() {
        int picker = Random.Range(0, numFarAttackEnemy);
        if(picker == 0) {
            currentEnemyPrefab = enemyPrefab2;
        }
        else {
            currentEnemyPrefab = enemyPrefab1;
        }

        Vector3 enemyPosition;
        enemyPosition.x = transform.position.x;
        enemyPosition.y = transform.position.y;
        enemyPosition.z = transform.position.z;

        GameObject spawnedEnemy = Instantiate(currentEnemyPrefab, enemyPosition, transform.rotation) as GameObject;
        spawnedEnemy.transform.parent = gameObject.transform;
    }
}
