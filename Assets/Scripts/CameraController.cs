using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 TargetPosition;

    public Rect LevelBounds = new Rect(-16, 0, 32, 18);

    public float DampingFactor = 10.0f;

    public float Distance = 64.0f;

    public Transform cameraContainer;

    private static readonly float fovY = Mathf.Deg2Rad * 20.0f;
    private static readonly float fovX = 16.0f / 9.0f * fovY;

    private void FixedUpdate()
    {
        var target = TargetPosition;

        // compute the screen size at z = 0
        var projectedHalfScreen = new Vector2(Mathf.Tan(fovX * 0.5f), Mathf.Tan(fovY * 0.5f)) * Distance;

        // pad the level bounds to get a safe target area
        var minPosition = LevelBounds.min + projectedHalfScreen;
        var maxPosition = LevelBounds.max - projectedHalfScreen;

        // if max - min is negative, it means the level is smaller than the screen area
        // center it in this case
        var negativeOffset = Vector2.Min(maxPosition - minPosition, new Vector2(0.0f, 0.0f));
        minPosition += negativeOffset * 0.5f;
        maxPosition -= negativeOffset * 0.5f;

        // clamp the target to the safe area
        target = Vector2.Max(target, minPosition);
        target = Vector2.Min(target, maxPosition);

        // smooth the movement over time
        var smoothPosition = Vector2.Lerp(new Vector2(cameraContainer.position.x, cameraContainer.position.y), target, Mathf.Min(Time.deltaTime * DampingFactor, 1.0f));

        cameraContainer.position = new Vector3(smoothPosition.x, smoothPosition.y, -Distance);
    }
}
