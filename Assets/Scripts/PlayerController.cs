using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed = 10.0f;
    public float InputSensitivity = 80.0f;
    public float JumpVelocity = 12.0f;
    public float AnimationSpeed = 0.01f;
    private Rigidbody2D rigidBody;
    public FeetCollider FeetCollider;

    public Transform FrameContainer;

    private bool jumping = false;

    private float animationTime = 0;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // update animation when walking
        if (FeetCollider.Grounded)
        {
            float animationDelta = Mathf.Abs(rigidBody.velocity.x) * AnimationSpeed;
            float newAnimationTime = (animationTime + animationDelta) % (float)FrameContainer.childCount;

            int currentFrame = (int)animationTime;
            int nextFrame = (int)newAnimationTime;
            if (currentFrame != nextFrame)
            {
                FrameContainer.GetChild(currentFrame).gameObject.SetActive(false);
                FrameContainer.GetChild(nextFrame).gameObject.SetActive(true);
            }

            animationTime = newAnimationTime;
        }

        // flip with direction
        if (Mathf.Abs(rigidBody.velocity.x) > 0.2)
            FrameContainer.localScale = new Vector3(rigidBody.velocity.x >= 0.0f ? 1.0f : -1.0f, 1.0f, 1.0f);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rigidBody.velocity;

        var horizontal = Input.GetAxis("Horizontal");

        // dead zone
        if (Mathf.Abs(horizontal) < 0.1f)
            horizontal = 0.0f;

        float targetSpeed = horizontal * MaxSpeed;
        velocity.x = Mathf.Lerp(velocity.x, targetSpeed, Mathf.Min(Time.deltaTime * InputSensitivity, 1.0f));

        var jumpPressed = Input.GetButton("Jump");
        if (jumpPressed && !jumping && FeetCollider.Grounded)
        {
            jumping = true;
            velocity.y = JumpVelocity;
        }
        else if (!jumpPressed)
        {
            jumping = false;
        }

        rigidBody.velocity = velocity;
    }
}
