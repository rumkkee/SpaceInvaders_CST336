using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHiveBrain : MonoBehaviour
{
    public float horizontalNudgeDistance;
    public float verticalNudgeDistance;

    public float startNudgeSpeed;
    public float maxNudgeSpeed;

    public int enemiesPerWave;
    public float distanceBetweenEnemies;

    public GameObject leftWall;
    public GameObject rightWall;

    public List<Enemy> waves;

    private void Awake()
    {
        transform.position = new Vector2(leftWall.transform.position.x + distanceBetweenEnemies, transform.position.y);
        if (waves != null)
        {
            InstantiateWaves();
        }
    }

    private void InstantiateWaves()
    {
        float currentWaveHeight = 0f;
        foreach(Enemy enemyTypeByWave in waves)
        {
            // TODO: Instantiate enemies for the given wave
            for(int i = 0; i < enemiesPerWave; i++)
            {
                Vector2 spawnPos = new Vector2(i * distanceBetweenEnemies + transform.position.x, currentWaveHeight + transform.position.y);
                //spawnPos = new Vector2(spawnPos.x + (enemiesPerWave/i * horizontalNudgeDistance), spawnPos.y);
                Enemy enemy = Instantiate(enemyTypeByWave, spawnPos, Quaternion.identity, transform);
            }
            currentWaveHeight -= verticalNudgeDistance;
        }
    }
}
