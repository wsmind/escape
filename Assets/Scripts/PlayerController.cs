using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed = 10.0f;
    public float InputSensitivity = 80.0f;
    public float JumpVelocity = 12.0f;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 velocity = rigidBody.velocity;

        var horizontal = Input.GetAxis("Horizontal");

        // dead zone
        if (Mathf.Abs(horizontal) < 0.1f)
            horizontal = 0.0f;

        float targetSpeed = horizontal * MaxSpeed;
        velocity.x = Mathf.Lerp(velocity.x, targetSpeed, Mathf.Min(Time.deltaTime * InputSensitivity, 1.0f));

        var jumpPressed = Input.GetButtonDown("Jump");
        if (jumpPressed)
            velocity.y = JumpVelocity;

        rigidBody.velocity = velocity;
    }
}
