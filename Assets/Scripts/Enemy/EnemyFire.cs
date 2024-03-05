using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyFire : MonoBehaviour
{
    public float upperFireDelay;
    public float lowerFireDelay;
    public EnemyProjectile projectilePrefab;

    private IEnumerator Start()
    {
        while (true)
        {
            float delay = Random.Range(lowerFireDelay, upperFireDelay);
            yield return new WaitForSeconds(delay);
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        }
    }
}
