using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed = 4.0f;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        // dead zone
        if (Mathf.Abs(horizontal) < 0.1f)
            horizontal = 0.0f;

        rigidBody.velocity = new Vector2(horizontal * MaxSpeed, rigidBody.velocity.y);
    }
}
