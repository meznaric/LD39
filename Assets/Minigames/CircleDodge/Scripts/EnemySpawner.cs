using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public int enemyCount = 4;
    public int spawnWait = 2;

    public GameObject Enemy;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves() {
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(-4, 4), transform.position.z);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(Enemy, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }
        }
}
}
