using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform[] targets;

    public Vector3 offset;
    public float smoothTime = 0.5f;
    public float minZoom = 4f;
    public float maxZoom = 9f;

    private Camera cam;
    private Vector3 velocity;
    private Bounds bounds;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Length == 0) return;

        CalculateBounds();
        MoveCamera();
        ZoomCamera();
    }

    private void CalculateBounds()
    {
        bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 1; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
    }

    private void MoveCamera()
    {
        Vector3 desiredPosition = bounds.center + offset;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    private void ZoomCamera()
    {
        float desiredZoom = Mathf.Max(minZoom, Mathf.Min(maxZoom, bounds.size.x / cam.aspect));
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredZoom, smoothTime);
    } 
}
