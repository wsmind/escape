using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 TargetPosition;

    public Rect LevelBounds = new Rect(-16, 0, 32, 18);

    public float DampingFactor = 10.0f;

    public float Distance = 36.0f;

    public Transform cameraContainer;

    private void FixedUpdate()
    {
        var position = Vector2.Lerp(new Vector2(cameraContainer.position.x, cameraContainer.position.y), TargetPosition, Mathf.Min(Time.deltaTime * DampingFactor, 1.0f));

        cameraContainer.position = new Vector3(position.x, position.y, -Distance);
    }
}
