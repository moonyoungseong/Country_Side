using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelCameraControl : MonoBehaviour
{
    private Transform target;
    public Transform voxeltarget;
    public Transform housetarget;
    public float transitionTime = 2f; // transition time in seconds

    private bool key;

    Vector3 targetPosition;

    private void Start()
    {
        transform.position = new Vector3(0, 10, -20);
        //transform.LookAt(voxeltarget);
        key = false;
        targetPosition = transform.position;
    }

    void Update()
    {
        LookAtTarget();
        CheckForKeyPress();
        SmoothTransition();
    }

    void CheckForKeyPress()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            targetPosition = new Vector3(0, 20, -40);
            key = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            targetPosition = new Vector3(-40, 20, 0);
            key = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            targetPosition = new Vector3(0, 20, 40);
            key = true;
        }
        else if (Input.GetMouseButtonDown(2))
        {
            targetPosition = new Vector3(0, 10, -20);
            key = false;
        }
    }

    void SmoothTransition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / transitionTime);
    }

    void LookAtTarget()
    {
        if(key)
        {
            transform.LookAt(housetarget);
        }
        if (!key)
        {
            transform.LookAt(voxeltarget);
        }
    }
}
