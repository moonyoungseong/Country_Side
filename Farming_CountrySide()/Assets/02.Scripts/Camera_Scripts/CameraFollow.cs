using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CameraRotate cameraRotate;
    public GameObject target;

    public float delayTime = 1;

    private float xOffset;
    private float yOffset;
    private float zOffset;

    private bool isKeyPressed = false;

    void Awake()
    {
        cameraRotate = GetComponent<CameraRotate>();
    }

    void Start()
    {
        xOffset = 0.0f;
        yOffset = 12.0f;
        zOffset = -10.0f;
    }
    void Update()
    {
        FollowTarget();
    }
    void LateUpdate()
    {
        // If the 1 key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Check whether the key has been pressed before
            if (isKeyPressed)
            {
                xOffset = 0.0f;
                yOffset = 4.0f;
                zOffset = -2.5f;

            }
            else
            {
                xOffset = 0.0f;
                yOffset = 12.0f;
                zOffset = -10.0f;
            }

            // Flip the boolean
            isKeyPressed = !isKeyPressed;
        }
    }

    public void FollowTarget()
    {
        if (target == null)
        {
            return;
        }

        Vector3 fixedPos = new Vector3(
            target.transform.position.x + xOffset,
            target.transform.position.y + yOffset,
            target.transform.position.z + zOffset
        );

        transform.position = Vector3.Lerp(transform.position, fixedPos, Time.deltaTime * delayTime);

        transform.LookAt(target.transform.position);
    }
}
