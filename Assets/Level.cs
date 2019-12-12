using UnityEngine;

public class Level : MonoBehaviour
{
    private PlayerController player;

    private CameraController cameraController;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
        player = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();

        if (player == null)
            Debug.LogError("No player found in this level!");
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            cameraController.TargetPosition = new Vector3(Mathf.Sin(Time.time), Mathf.Sin(Time.time * 0.7f), 0.0f);
            return;
        }

        var playerPosition = player.gameObject.transform.position;

        cameraController.TargetPosition = new Vector3(playerPosition.x, playerPosition.y, 0.0f);
    }
}
