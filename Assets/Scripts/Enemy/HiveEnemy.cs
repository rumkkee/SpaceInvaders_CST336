using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveEnemy : Enemy
{
    public delegate void HiveEnemyDestroyed(Enemy enemy);
    public static event HiveEnemyDestroyed OnHiveEnemyDestroyed;

    private void OnDestroy()
    {
        OnHiveEnemyDestroyed(this);
    }
}
