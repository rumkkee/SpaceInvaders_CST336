using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Vector2.down * _projectileSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Defeat();
            Destroy(this.gameObject);
        }
    }
}
