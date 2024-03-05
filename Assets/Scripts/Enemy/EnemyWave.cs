using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public GameObject enemyPrefab;
    private void SpawnEnemy(Vector2 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity, transform);
    }
}
