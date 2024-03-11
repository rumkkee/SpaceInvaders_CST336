using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _pointValue;

    public delegate void EnemyDefeated(Enemy enemy);
    public static event EnemyDefeated OnDefeat;

    public void Defeat()
    {
        DeathCalls();
        ParticlesManager.instance.InstantiateParticles(ParticlesManager.instance.EnemyParticles(), transform.position);
        AudioManager.instance.PlayEnemyDefeated();
        Destroy(this.gameObject);
    }

    public virtual void DeathCalls()
    {
        OnDefeat(this);
    }

    public int GetPoints() => _pointValue;
}
