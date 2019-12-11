using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    public float DampingFactor = 10.0f;

    private void Awake()
    {
        if (player == null)
            Debug.LogError("You need to attach the player to the camera rig!");
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            transform.position = new Vector3(Mathf.Sin(Time.time), Mathf.Sin(Time.time * 0.7f), 0.0f);
            return;
        }

        var playerPosition = player.gameObject.transform.position;

        var targetPosition = new Vector3(playerPosition.x, playerPosition.y, 0.0f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Mathf.Min(Time.deltaTime * DampingFactor, 1.0f));
    }
}
