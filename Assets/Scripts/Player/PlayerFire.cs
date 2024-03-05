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
            _projectileInstance = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, this.transform);
        }
    }
}
