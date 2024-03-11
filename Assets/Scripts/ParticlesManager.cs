using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem enemyParticlesPrefab;
    [SerializeField] private ParticleSystem playerParticlesPrefab;

    public static ParticlesManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void InstantiateParticles(ParticleSystem particlesPrefab, Vector3 spawnPos)
    {
        Instantiate(particlesPrefab, spawnPos, Quaternion.identity, transform);
    }

    public ParticleSystem EnemyParticles() => enemyParticlesPrefab;
    public ParticleSystem PlayerParticles() => playerParticlesPrefab;
}
