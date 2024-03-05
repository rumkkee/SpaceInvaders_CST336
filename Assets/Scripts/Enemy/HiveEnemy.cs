using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveEnemy : Enemy
{
    public delegate void HiveEnemyDestroyed(Enemy enemy);
    public static event HiveEnemyDestroyed OnHiveEnemyDestroyed;

    public delegate void HitSideWall(Vector2 wallNormal);
    public static event HitSideWall OnWallHit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        SideWall wall = other.gameObject.GetComponent<SideWall>();
        if (wall != null)
        {
            OnWallHit(wall.normal);
        }
    }

    public override void DeathCalls()
    {
        base.DeathCalls();
        OnHiveEnemyDestroyed(this);
    }
}
