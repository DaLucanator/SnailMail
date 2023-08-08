using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MobileCameraTranslate : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] float moveSpeed, playerDrag;
    [SerializeField] Rigidbody rb;

    [SerializeField] private PlayerInput playerInput;
    private InputAction primaryFingerTouch, secondaryFingerTouch;

    [SerializeField] private TextMeshProUGUI touchpos1text, touchpos2text;

    private Vector2 touchPos1, touchPos2;
    private Vector3 previousPosition;
    private bool isTouching1, isTouching2;
    private bool canStartMove = true;
    int fingerToTrack;

    private void Awake()
    {
        primaryFingerTouch = playerInput.actions["PrimaryFingerTouch"];
        secondaryFingerTouch = playerInput.actions["SecondaryFingerTouch"];
    }

    private void Start()
    {
        EventManager.current.EnableMobileMovement += Enable;
        EventManager.current.DisableMobileMovement += Disable;
    }

    private void OnEnable()
    {
        primaryFingerTouch.performed += PrimaryFingerTouch;
        secondaryFingerTouch.performed += SecondaryFingerTouch;
    }

    void PrimaryFingerTouch(InputAction.CallbackContext context)
    {
        touchPos1 = context.ReadValue<Vector2>();
        touchpos1text.text = touchPos1.ToString();
    }

    void SecondaryFingerTouch(InputAction.CallbackContext context)
    {
        touchPos2 = context.ReadValue<Vector2>();
    }

    private void Disable()
    {
        this.enabled = false;
    }

    private void Enable()
    {
        this.enabled = true;
    }

    private void Update()
    {
        isTouching1 = primaryFingerTouch.WasPerformedThisFrame();
        isTouching2 = secondaryFingerTouch.WasPerformedThisFrame();
    }

    private void FixedUpdate()
    {
        if ((isTouching1 || isTouching2) && canStartMove)
        {
            if (isTouching1)
            {
                previousPosition = cam.ScreenToViewportPoint(touchPos1);
                fingerToTrack = 1;
                canStartMove = !CheckIfOnRightSide(touchPos1.x);
            }
            else if (isTouching2)
            {
                previousPosition = cam.ScreenToViewportPoint(touchPos2);
                fingerToTrack = 2;
                canStartMove = !CheckIfOnRightSide(touchPos2.x);
            }
        }
        else if ((isTouching1 || isTouching2) && !canStartMove)
        {
            Vector3 newPosition = previousPosition;

            if (fingerToTrack == 1 && isTouching1)
            {
                newPosition = cam.ScreenToViewportPoint(touchPos1);

            }
            else if (fingerToTrack == 2 && isTouching2)
            {
                newPosition = cam.ScreenToViewportPoint(touchPos2);
            }

            Vector3 direction = previousPosition - newPosition;
            direction = direction.normalized;

            direction.z = direction.y;
            direction.y = 0f;

            //changed to physics based movement so camera/player can collide with walls
            //transform.Translate(direction * Time.deltaTime * moveSpeed);

            MoveCamera(direction);
            SpeedControl();

            previousPosition = newPosition;
        }

        if (!isTouching1 && fingerToTrack == 1)
        {
            fingerToTrack = 0;
            canStartMove = true;
        }
        else if (!isTouching2 && fingerToTrack == 2)
        {
            fingerToTrack = 0;
            canStartMove = true;
        }
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

    private bool CheckIfOnRightSide(float xCoord)
    {
        if (xCoord > Screen.width / 2) { return true; }
        else return false;
    }
}
