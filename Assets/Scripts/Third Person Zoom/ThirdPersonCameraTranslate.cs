using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ThirdPersonCameraTranslate : MonoBehaviour
{
    [SerializeField] private float moveSpeed, rotateTime;

    private float horizontalInput, verticalInput;

    void Update()
    {
        Vector3 moveDirection = (Vector3.forward * verticalInput) + (Vector3.right * horizontalInput);

        transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 moveVector2 = inputValue.Get<Vector2>();
        horizontalInput = moveVector2.x;
        verticalInput = moveVector2.y;

        Debug.Log("boop");
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
