using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MobileCameraRotate : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform rotationAnchor;
    [SerializeField] private float rotationModifier;
    [SerializeField] private TextMeshProUGUI touchpos1text, touchpos2text;

    [SerializeField] private PlayerInput playerInput;
    private InputAction primaryFingerTouch, secondaryFingerTouch;

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
        touchpos2text.text = touchPos2.ToString();
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

        touchpos2text.text = Screen.width.ToString();

        if ((isTouching1 || isTouching2) && canStartMove)
        {
            if (isTouching1) 
            {
                previousPosition = cam.ScreenToViewportPoint(touchPos1);
                fingerToTrack = 1;
                canStartMove = !CheckIfOnLeftSide(touchPos1.x);
            }
            else if (isTouching2) 
            {
                previousPosition = cam.ScreenToViewportPoint(touchPos2);
                fingerToTrack = 2;
                canStartMove = !CheckIfOnLeftSide(touchPos2.x);
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

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            rotationAnchor.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

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

    private bool CheckIfOnLeftSide(float xCoord)
    {
        if (xCoord < Screen.width / 2) { return true; }
        else return false;
    }

}
