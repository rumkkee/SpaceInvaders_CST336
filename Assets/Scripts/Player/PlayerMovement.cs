using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveDirection;
    public float moveSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<float>();
        if(moveDirection == 0)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.right * moveDirection * moveSpeed);
    }
}
