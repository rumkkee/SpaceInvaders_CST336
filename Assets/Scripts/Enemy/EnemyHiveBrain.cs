using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHiveBrain : MonoBehaviour
{
    [Header("Enemy Instantiation")]
    public int enemiesPerWave;
    public float distanceBetweenEnemies;
    
    [Header("Enemy Movement: Increment")]
    public float horizontalNudgeDistance;
    public float verticalNudgeDistance;

    [Header("Enemy Movement: Rate")]
    public float startNudgeDelay;
    public float endNudgeDelay;
    public float currentNudgeDelay;

    [Header("Enemy Fire")]
    public float upperFireDelay;
    public float lowerFireDelay;

    [Header("Wave List / Enemy Type")]
    public List<Enemy> waves;

    private int _startingEnemyCount;
    private List<Enemy> _enemies;

    private Vector2 _moveDirection;

    public static EnemyHiveBrain instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        HiveEnemy.OnWallHit += OnWallHit;
        HiveEnemy.OnHiveEnemyDestroyed += OnEnemyDefeated;
    }

    #region Wave Setup
    public void StartRound()
    {
        float leftWallPosX = ResourceManager.instance.leftWall.transform.position.x;
        transform.position = new Vector2(leftWallPosX + distanceBetweenEnemies, transform.position.y);
        _enemies = new List<Enemy>();
        if (waves != null)
        {
            currentNudgeDelay = startNudgeDelay;
            InstantiateWaves();
            _moveDirection = Vector2.right;
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
                _enemies.Add(enemy);
                _startingEnemyCount++;
            }
            currentWaveHeight -= verticalNudgeDistance * 2;
        }
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
        float horizontalNudge = (_moveDirection == Vector2.right) ? horizontalNudgeDistance : -horizontalNudgeDistance;
        transform.position = new Vector2(transform.position.x + horizontalNudge, transform.position.y);
    }

    private void OnWallHit(Vector2 newDirection)
    {
        // Accounts for the event that multiple enemies hit the wall
        if(_moveDirection != newDirection)
        {
            _moveDirection = newDirection;
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
        float currentEnemyCount = _enemies.Count;
        currentNudgeDelay = Mathf.Lerp(endNudgeDelay, startNudgeDelay, currentEnemyCount / (float)_startingEnemyCount);
    }

    

    #endregion

    #region Firing
    private IEnumerator FireTimer()
    {
        while (true)
        {
            float delay = Random.Range(lowerFireDelay, upperFireDelay);
            yield return new WaitForSeconds(delay);
            int enemyCount = _enemies.Count;
            int randIndex = Random.Range(0, enemyCount);
            _enemies[randIndex].GetComponent<EnemyFire>().Fire();
        }
    }
    #endregion

    #region Enemy Death
    private void OnEnemyDefeated(Enemy enemy)
    {
        RemoveEnemy(enemy);
        ReviseCurrentNudgeDelay();      
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        if(_enemies.Count == 0)
        {
            EnemyManager.instance.EndRound();
        }
    }
    #endregion

}
