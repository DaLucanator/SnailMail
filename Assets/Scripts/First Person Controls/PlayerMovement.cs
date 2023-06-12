using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed, playerHeight, playerDrag;

    private float horizontalInput, verticalInput;

    private Rigidbody rb;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation= true;
        rb.drag = playerDrag;
    }

    void FixedUpdate()
    {
        GroundCheck();
        MovePlayer();
        SpeedControl();
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 moveVector2 = inputValue.Get<Vector2>();
        horizontalInput = moveVector2.x;
        verticalInput = moveVector2.y;
    }

    private void GroundCheck()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight / 2f) + 0.2f);

        //turn drag off when I'm in the air
        if (!grounded) { rb.drag = 0f; }
        else { rb.drag = playerDrag; }
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);

        if(flatVelocity.magnitude > moveSpeed) 
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3 (limitedVelocity.x, rb.velocity.y, limitedVelocity.z); 
        }
    }
}
