using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _pointValue;

    public delegate void EnemyDefeated(int points);
    public static event EnemyDefeated OnDefeat;

    public void Defeat()
    {
        OnDefeat(_pointValue);
        Destroy(this.gameObject);
    }
}
