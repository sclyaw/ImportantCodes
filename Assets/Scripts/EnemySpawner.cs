using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnInterval = 6f; 
    public int maxEnemies = 10; 

    private int currentEnemyCount = 0;

    public GameObject spawnEffectPrefab;


    void Start()
    {
        
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount < maxEnemies)
        {
            float randomX = Random.Range(transform.position.x-2f, transform.position.x + 2f);

            float randomY = Random.Range(transform.position.y - 2f, transform.position.y + 2f);
            Vector3 spawnPosition = new Vector2(randomX, randomY);

            StartCoroutine(SpawnEnemyWithEffect(spawnPosition));

            
        }
    }

    public void EnemyDestroyed()
    {
        
        currentEnemyCount--;
    }

    IEnumerator SpawnEnemyWithEffect(Vector2 spawnPosition)
    {
        // Efekti spawn noktasýnda oluþtur
        GameObject hitEffect = Instantiate(spawnEffectPrefab, new Vector2(spawnPosition.x, spawnPosition.y + 3f), Quaternion.identity);

        if (hitEffect != null)
        {

            Animator hitAnimator = hitEffect.GetComponent<Animator>();

            if (hitAnimator != null)
            {
                Destroy(hitEffect, hitAnimator.GetCurrentAnimatorStateInfo(0).length);
                yield return new WaitForSeconds(1f);
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                currentEnemyCount++;
                
            }
        }

        
        

        

       
       
    }


    
}
