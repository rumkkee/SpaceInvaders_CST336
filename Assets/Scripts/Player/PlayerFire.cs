using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerFire : MonoBehaviour
{
    [SerializeField] private PlayerProjectile _projectilePrefab;

    private PlayerProjectile _projectileInstance;

    private void OnFire(InputValue value)
    {
        if(_projectileInstance == null)
        {
            Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y + 1f);
            _projectileInstance = Instantiate(_projectilePrefab, spawnPos, Quaternion.identity, this.transform);
        }
    }
}
