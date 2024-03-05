using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _pointValue;

    public delegate void EnemyDefeated(int points);
    public static event EnemyDefeated OnDefeat;

    public delegate void HitSideWall(Vector2 wallNormal);
    public static event HitSideWall OnWallHit;

    public void Defeat()
    {
        OnDefeat(_pointValue);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SideWall wall = other.gameObject.GetComponent<SideWall>();
        if(wall != null)
        {
            OnWallHit(wall.normal);
        }
    }
}
