using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Inspector Assigned
    [SerializeField] private GameObject EnemyPrefab;

    //Public
    public List<AIState> _enemies = new List<AIState>();
    public int totalWaves = 2; //No of Towers == No of Waves
    public int currentWave =  0;
    public int totalEnemiesPerWave;
    public int enemiesOnScreen;
    public int maxEnemiesOnScreen = 6; 
    public Transform[] spawnPoints;

    public int minEnemies = 5;   // per wave //
    public int maxEnemies = 8; // per wave //

    public bool waveDone = false;

    private int index;
 

    public void Awake()
    {
         
    }

    public void Update()
    {
      
    }

    private void SpawningEnemies()
    {
        GameObject enemy = Instantiate(EnemyPrefab);
        Vector3 enemyPosition = Vector3.zero;
        index = Random.Range(0, spawnPoints.Length);
        enemyPosition = spawnPoints[index].position;
        enemy.transform.position = enemyPosition;
        _enemies.Add(enemy.GetComponent<AIState>());
    }

    public void CalculatingEnemies()
    {
        int enemiesPerWave = Random.Range(minEnemies, maxEnemies);
        for(;currentWave < totalWaves;currentWave++)
        {
            if (!waveDone)
            {
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    SpawningEnemies();
                    waveDone = true;
                }
            }
        }
    }

    public void RemoveDeadEnemy(AIState deadEnemy)
    {
        _enemies.Remove(deadEnemy);
    }
}
