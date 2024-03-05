using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyFire : MonoBehaviour
{
    public EnemyProjectile projectilePrefab;

    public void Fire()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
