using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if(projectile != null)
        {
            Destroy(projectile.gameObject);
            ParticlesManager particlesManager = ParticlesManager.instance;
            particlesManager.InstantiateParticles(particlesManager.BarrierParticles(), transform.position);
            Destroy(gameObject);
        }

    }
}
