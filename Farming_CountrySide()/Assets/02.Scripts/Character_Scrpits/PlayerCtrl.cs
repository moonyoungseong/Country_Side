using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody rb;

    public float turnSpeed = 80.0f;

    private Animator anim;
    private new Transform transform;
    private Vector3 moveDir;
    private readonly int hashWalk = Animator.StringToHash("Walk");

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        StartCoroutine(ApplyGravityWhenNotMoving());
    }

    void FixedUpdate()
    {
        if (moveDir != Vector3.zero)
        {
            MovePlayer();
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();

        moveDir = (Camera.main.transform.forward * dir.y + Camera.main.transform.right * dir.x).normalized;
        moveDir.y = 0;

        // Rotate character to face movement direction
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }

        anim.SetBool(hashWalk, dir.magnitude > 0);

        Debug.Log($"Move= ({dir.x},{dir.y})");
    }

    private void MovePlayer()
    {
        Vector3 playerposition = rb.position + moveDir.normalized * 1.2f * Time.fixedDeltaTime;
        rb.MovePosition(playerposition);
    }

    IEnumerator ApplyGravityWhenNotMoving()
    {
        while (true)
        {
            if (moveDir == Vector3.zero)
            {
                anim.SetBool(hashWalk, false);
                rb.AddForce(12 * Physics.gravity, ForceMode.Acceleration); // This line applies a force downward
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
    /*
    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        Quaternion playerrotation = rb.rotation * Quaternion.Euler(Vector3.up * mouseX);
        rb.MoveRotation(playerrotation);
    }
    */
}
