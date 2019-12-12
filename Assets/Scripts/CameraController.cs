using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 TargetPosition;

    public float DampingFactor = 10.0f;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, Mathf.Min(Time.deltaTime * DampingFactor, 1.0f));
    }
}
