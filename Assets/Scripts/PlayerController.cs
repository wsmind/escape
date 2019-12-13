using System;
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

    public Transform SpriteParent;
    public Transform FrameContainer;

    public AudioSource walkAudio;
    public AudioSource jumpAudio;

    public GameObject[] PotionContainers;
    public bool HasPotion { get; private set; } = false;

    public Vector2 PointOfInterest { get; private set; }

    private bool jumping = false;

    private float animationTime = 0;

    private ActivationZone currentAnimationZone;

    public void SwitchLayer(int layer)
    {
        gameObject.layer = layer;
        FeetCollider.SwitchLayer(layer);
    }

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
                walkAudio.pitch = UnityEngine.Random.value * 0.3f + 4.0f;
                walkAudio.Play();
            }

            animationTime = newAnimationTime;
        }

        // flip with direction
        if (Mathf.Abs(rigidBody.velocity.x) > 0.2)
            SpriteParent.localScale = new Vector3(rigidBody.velocity.x >= 0.0f ? 1.0f : -1.0f, 1.0f, 1.0f);

        // damped point of interested (for camera targeting)
        var targetPoi = new Vector2(SpriteParent.localScale.x * 6.0f + 0.5f, 1.5f);
        PointOfInterest = Vector2.Lerp(PointOfInterest, targetPoi, Mathf.Min(Time.deltaTime * 0.5f, 1.0f));

        // activations
        var activatePressed = Input.GetButtonDown("Activate");
        if (currentAnimationZone && activatePressed)
            currentAnimationZone.Activate();
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
            jumpAudio.pitch = UnityEngine.Random.value * 0.2f + 1.5f;
            jumpAudio.Play();
        }
        else if (!jumpPressed)
        {
            jumping = false;
        }

        rigidBody.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var activationZone = other.GetComponent<ActivationZone>();
        if (activationZone && !currentAnimationZone && activationZone.CanActivate(this))
        {
            currentAnimationZone = activationZone;
            activationZone.Show();
        }

        var bumper = other.GetComponent<Bumper>();
        if (bumper != null)
        {
            jumping = true;

            Vector2 velocity = rigidBody.velocity;
            velocity.y = bumper.BumpVelocity;
            rigidBody.velocity = velocity;

            jumpAudio.pitch = UnityEngine.Random.value * 0.2f + 0.8f;
            jumpAudio.Play();
        }

        var potion = other.GetComponent<Potion>();
        if (potion != null)
        {
            HasPotion = true;
            potion.Take();
            Array.ForEach(PotionContainers, obj => obj.SetActive(true));

            jumpAudio.pitch = UnityEngine.Random.value * 0.2f + 3.8f;
            jumpAudio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var activationZone = other.GetComponent<ActivationZone>();
        if (activationZone && (activationZone == currentAnimationZone))
        {
            activationZone.Hide();
            currentAnimationZone = null;
        }
    }
}
