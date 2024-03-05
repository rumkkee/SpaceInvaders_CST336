using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class UFO : Enemy
{
    public List<int> pointValues;

    public float _moveSpeed;

    private void Awake()
    {
        int randIndex = Random.Range(0, pointValues.Count);
        _pointValue = pointValues[randIndex];
    }

    public void SetImpulse(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * _moveSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        SideWall wall = other.gameObject.GetComponent<SideWall>();
        if (wall != null)
        {
            Destroy(this.gameObject);
        }
    }
}
