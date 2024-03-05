using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if(projectile != null)
        {
            Destroy(projectile.gameObject);
            Destroy(gameObject);
        }

    }
}
