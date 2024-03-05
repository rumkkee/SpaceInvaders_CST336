using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCatcher : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerProjectile projectile = other.gameObject.GetComponent<PlayerProjectile>();
        if(projectile != null)
        {
            Destroy(projectile.gameObject);
        }
    }
}
