using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHiveBrain : MonoBehaviour
{
    [Header("Enemy Movement")]
    public float horizontalNudgeDistance;
    public float verticalNudgeDistance;

    public float startNudgeDelay;
    public float endNudgeDelay;
    public float currentNudgeDelay;

    [Header("Enemy Instantiation")]
    public int enemiesPerWave;
    public float distanceBetweenEnemies;

    public GameObject leftWall;
    public GameObject rightWall;

    public List<Enemy> waves;

    public List<Enemy> enemies;

    [Header("Enemy Fire Params")]
    public float upperFireDelay;
    public float lowerFireDelay;

    public int startingEnemyCount;
    public int enemyCount;

    private Vector2 direction;

    public static EnemyHiveBrain instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Enemy.OnWallHit += OnWallHit;
        Enemy.OnDefeat += OnEnemyDefeated;
        HiveEnemy.OnHiveEnemyDestroyed += RemoveEnemy;
    }

    #region Setup
    public void StartRound()
    {
        transform.position = new Vector2(leftWall.transform.position.x + distanceBetweenEnemies, transform.position.y);
        enemies = new List<Enemy>();
        if (waves != null)
        {
            currentNudgeDelay = startNudgeDelay;
            InstantiateWaves();
            direction = Vector2.right;
        }
        StartCoroutine(VerticalMovement());
        StartCoroutine(FireTimer());
    }

    private void InstantiateWaves()
    {
        float currentWaveHeight = 0f;
        foreach(Enemy enemyTypeByWave in waves)
        {
            for(int i = 0; i < enemiesPerWave; i++)
            {
                Vector2 spawnPos = new Vector2(i * distanceBetweenEnemies + transform.position.x, currentWaveHeight + transform.position.y);
                Enemy enemy = Instantiate(enemyTypeByWave, spawnPos, Quaternion.identity, transform);
                enemies.Add(enemy);
                startingEnemyCount++;
            }
            currentWaveHeight -= verticalNudgeDistance * 2;
        }
        enemyCount = startingEnemyCount;
    }
    #endregion

    #region Movement

    private IEnumerator VerticalMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentNudgeDelay);
            NudgeHiveHorizontal();
        }
    }

    private void NudgeHiveHorizontal()
    {
        float horizontalNudge = (direction == Vector2.right) ? horizontalNudgeDistance : -horizontalNudgeDistance;
        transform.position = new Vector2(transform.position.x + horizontalNudge, transform.position.y);
    }

    private void OnWallHit(Vector2 newDirection)
    {
        // Accounts for the event that multiple enemies hit the wall
        if(direction != newDirection)
        {
            direction = newDirection;
            NudgeHiveVertical();
            NudgeHiveHorizontal();
        }
    }
    private void NudgeHiveVertical()
    {
        transform.localPosition = new Vector2(transform.position.x, transform.localPosition.y - verticalNudgeDistance);
    }

    private void ReviseCurrentNudgeDelay()
    {
        currentNudgeDelay = Mathf.Lerp(endNudgeDelay, startNudgeDelay, enemyCount / (float)startingEnemyCount);
    }

    private void OnEnemyDefeated(int points)
    {
        if(points <= 30)
        {
            enemyCount--;
            ReviseCurrentNudgeDelay();
        }
    }

    #endregion

    #region Firing
    private IEnumerator FireTimer()
    {
        while (true)
        {
            float delay = Random.Range(lowerFireDelay, upperFireDelay);
            yield return new WaitForSeconds(delay);
            int enemyCount = enemies.Count;
            int randIndex = Random.Range(0, enemyCount);
            Debug.Log("Enemy index: " + randIndex);
            enemies[randIndex].GetComponent<EnemyFire>().Fire();
        }
    }

    private void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count == 0)
        {
            EnemyManager.instance.EndRound();
        }
    }
    #endregion

    private void OnDestroy()
    {
        
    }
}