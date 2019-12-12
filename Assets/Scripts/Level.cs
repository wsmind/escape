using UnityEngine;

public class Level : MonoBehaviour
{
    public Rect Bounds = new Rect(-16, 0, 32, 18);

    private PlayerController player;

    private CameraController cameraController;

    private bool isDark = false;
    private float globalDarkness = 0.0f;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
        player = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();

        if (player == null)
            Debug.LogError("No player found in this level!");

        cameraController.LevelBounds = Bounds;

        foreach (var portal in GameObject.FindGameObjectsWithTag("Portal"))
        {
            portal.GetComponent<Portal>().OnPortalTriggered += OnPortalTriggered;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            cameraController.TargetPosition = new Vector2(Mathf.Sin(Time.time), Mathf.Sin(Time.time * 0.7f));
            return;
        }

        var playerPosition = player.gameObject.transform.position;
        cameraController.TargetPosition = new Vector2(playerPosition.x, playerPosition.y) + player.PointOfInterest;
    }

    private void Update()
    {
        var targetDarkness = isDark ? 1.0f : 0.0f;
        globalDarkness = Mathf.Lerp(globalDarkness, targetDarkness, Mathf.Min(5.0f * Time.deltaTime, 1.0f));
        Shader.SetGlobalFloat("GlobalDarkness", globalDarkness);
    }

    private void OnPortalTriggered()
    {
        isDark = !isDark;
    }
}
