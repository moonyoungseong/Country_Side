using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 10.0f;
    [SerializeField] private float minZoom = 10f;
    [SerializeField] private float maxZoom = 60f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (distance != 0)
        {
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView + distance, minZoom, maxZoom);
        }
    }
}
