using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Inspector Assigned
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private Text currentwaveText;
    [SerializeField] private GameObject waveObj;
    [SerializeField] private GameObject gameOverObj;

    //Public
    public List<AIState> _enemies = new List<AIState>();
    public int totalWaves = 1; //No of Towers == No of Waves
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
        gameOverObj.SetActive(false);
    }

    public void Update()
    {
        CalculatingEnemies();
    }

    private void SpawningEnemies()
    {
        GameObject enemy = Instantiate(EnemyPrefab);
        Vector3 enemyPosition = Vector3.zero;
        index = Random.Range(0, spawnPoints.Length);
        enemyPosition = spawnPoints[index].localPosition;
        enemy.transform.localPosition = enemyPosition;
        _enemies.Add(enemy.GetComponent<AIState>());
    }

    public void CalculatingEnemies()
    {
        currentwaveText.text = currentWave.ToString() + " / 6";
        if (waveDone)
        {
            if(_enemies.Count < 2)
            {
                waveDone = false;
                currentWave++;
                totalWaves++;
            }
            else
            {
                return;
            }
        }
        if(totalWaves == 7)
        {
            gameOverObj.SetActive(true);
            return;
        }
        int enemiesPerWave = Random.Range(minEnemies, maxEnemies);
        while(currentWave < totalWaves)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                waveObj.SetActive(true);
                SpawningEnemies();
                waveDone = true;
            }
            if (waveDone) break;
        }
    }

    public void RemoveDeadEnemy(AIState deadEnemy)
    {
        _enemies.Remove(deadEnemy);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0); // go to the StartMenu
    }




} //Class
