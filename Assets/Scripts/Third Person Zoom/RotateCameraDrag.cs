using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCameraDrag : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform rotationAnchor;
    
    private Vector3 previousPosition;
    [SerializeField] private bool canMove = true;

    [SerializeField] private PlayerInput playerInput;
    private InputAction rightClick;

    //Code from https://github.com/EmmaPrats/Camera-Rotation-Tutorial

    private void Awake()
    {
        rightClick = playerInput.actions["RightClick"];
    }

    void Update()
    {
        if (rightClick.WasPerformedThisFrame() && canMove)
        {
            previousPosition = cam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
            canMove = false;
        }
        else if (rightClick.ReadValue<float>() > 0f)
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
            Vector3 direction = previousPosition - newPosition;
            
            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically
            
            rotationAnchor.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!
            
            previousPosition = newPosition;
        }
        if (rightClick.WasReleasedThisFrame())
        {
            canMove = true;
        }
    }
}
