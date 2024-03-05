using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    #region Setup
    public void StartRound()
    {
        transform.position = new Vector2(leftWall.transform.position.x + distanceBetweenEnemies, transform.position.y);
        if (waves != null)
        {
            currentNudgeDelay = startNudgeDelay;
            InstantiateWaves();
            direction = Vector2.right;
        }
        StartCoroutine(VerticalMovement());
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
}
