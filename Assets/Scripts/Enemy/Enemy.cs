using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _pointValue;

    public delegate void EnemyDefeated(int points);
    public static event EnemyDefeated OnDefeat;

    public void Defeat()
    {
        DeathCalls();
        Destroy(this.gameObject);
    }

    public virtual void DeathCalls()
    {
        OnDefeat(_pointValue);
    }
}
