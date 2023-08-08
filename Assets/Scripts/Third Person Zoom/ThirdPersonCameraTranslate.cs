using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ThirdPersonCameraTranslate : MonoBehaviour
{
    [SerializeField] private float moveSpeed, playerDrag, rotateTime;
    [SerializeField] Rigidbody rb;

    private float horizontalInput, verticalInput;

    void FixedUpdate()
    {
        Vector3 moveDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);

        //changed to physics based movement so camera/player can collide with walls
        //transform.Translate(moveDirection * Time.deltaTime * moveSpeed);

        MoveCamera(moveDirection.normalized);
        SpeedControl();
    }

    private void MoveCamera(Vector3 directionToMove)
    {
        rb.AddForce(directionToMove * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 moveVector2 = inputValue.Get<Vector2>();
        horizontalInput = moveVector2.x;
        verticalInput = moveVector2.y;
    }

    private void OnRotateCameraRight()
    {
        StartCoroutine(RotateObject(180f,Vector3.up,rotateTime));
    }

    //code from https://answers.unity.com/questions/1220414/rotating-object-by-x-degree-once-every-y-seconds.html
    IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
    {
        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        // save starting rotation position
        Quaternion startRotation = transform.rotation;

        float deltaAngle = 0;

        // rotate until reaching angle
        while (deltaAngle < angle)
        {
            deltaAngle += rotationSpeed * Time.deltaTime;
            deltaAngle = Mathf.Min(deltaAngle, angle);

            transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

            yield return null;
        }
    }
}
