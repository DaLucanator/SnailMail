using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] Transform cam, camPosition;

    float mouseX;
    float mouseY;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        LookInput();

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void LookInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.fixedDeltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.fixedDeltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -87, 87);
    }
}
