using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time), Mathf.Sin(Time.time * 0.7f), 0.0f);
    }
}
