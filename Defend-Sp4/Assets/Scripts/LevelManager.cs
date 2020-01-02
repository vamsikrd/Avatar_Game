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
    [SerializeField] private Text currentWaveTimeText;

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

    public float nextWaveTime = 5f;
    public float currentWaveTime = 0f;
    private int currentWaveTimeInt;
    

    public bool waveDone = false;

    private int index;
 

    public void Awake()
    {
        gameOverObj.SetActive(false);
        currentWaveTime = nextWaveTime;
    }

    public void Update()
    {
        CalculatingEnemies();
        currentWaveTimeInt = (int)currentWaveTime;
        currentWaveTimeText.text = currentWaveTimeInt.ToString();
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
                currentWaveTime = nextWaveTime;
            }
            else
            {
                return;
            }
        }
        if(totalWaves == 7 && gameOverObj.activeInHierarchy == false)
        {
            gameOverObj.SetActive(true);
            return;
        }
        if (!waveDone)
        {
            currentWaveTime -= Time.deltaTime;
            if (currentWaveTime <= 0)
            {
                int enemiesPerWave = Random.Range(minEnemies, maxEnemies);
                while (currentWave < totalWaves)
                {
                    waveObj.SetActive(true);
                    for (int i = 0; i < enemiesPerWave; i++)
                    {
                        SpawningEnemies();
                        waveDone = true;
                    }
                    if (waveDone)
                    {
                        currentWaveTime = nextWaveTime;
                        break;
                    }
                }
            }
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
