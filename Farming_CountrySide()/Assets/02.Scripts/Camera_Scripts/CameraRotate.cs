using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float CamRotationSpeed = 1f;
    [SerializeField] private float minYAngle = -80f;  
    [SerializeField] private float maxYAngle = 50f;

    private float currentXAngle = 0f;
    private float currentYAngle = 0f;

    private bool isRotating = true; // flag to track camera rotation state

    private void Update()
    {
        if (Input.GetMouseButtonDown(2)) // check if middle mouse button is pressed
        {
            isRotating = !isRotating; // toggle camera rotation state
        }

        if (isRotating) // check if camera rotation is enabled
        {
            UpdateRotation();
        }
    }

    void UpdateRotation()
    {
        currentXAngle += Input.GetAxis("Mouse X") * CamRotationSpeed;
        currentYAngle -= Input.GetAxis("Mouse Y") * CamRotationSpeed;
        currentYAngle = Mathf.Clamp(currentYAngle, minYAngle, maxYAngle);

        transform.rotation = Quaternion.Euler(currentYAngle, currentXAngle, 0f);
    }
}
