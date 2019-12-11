using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;

    private void Awake()
    {
        if (player == null)
            Debug.LogError("You need to attach the player to the camera rig!");
    }

    private void Update()
    {
        if (player == null)
        {
            transform.position = new Vector3(Mathf.Sin(Time.time), Mathf.Sin(Time.time * 0.7f), 0.0f);
            return;
        }

        var playerPosition = player.gameObject.transform.position;

        transform.position = new Vector3(playerPosition.x, playerPosition.y, 0.0f);
    }
}
