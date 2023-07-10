using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class MobilePinchZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed, minFov, maxFov, minDistance;
    [SerializeField] private TextMeshProUGUI fovText, distanceText;

    public GameObject l, r;

    [SerializeField] private PlayerInput playerInput;
    private InputAction primaryFingerTouch, secondaryFingerTouch;


    bool isTouching1, isTouching2;
    Vector2 touchPos1,touchPos2;
    float distance;
    bool canStartZoom = true;

    private void Awake()
    {
        primaryFingerTouch = playerInput.actions["PrimaryFingerTouch"];
        secondaryFingerTouch = playerInput.actions["SecondaryFingerTouch"];
    }

    private void OnEnable()
    {
        primaryFingerTouch.performed += PrimaryFingerTouch;
        secondaryFingerTouch.performed += SecondaryFingerTouch;
    }

    void PrimaryFingerTouch(InputAction.CallbackContext context)
    {
        touchPos1 = context.ReadValue<Vector2>();
    }

    void SecondaryFingerTouch(InputAction.CallbackContext context)
    {
        touchPos2 = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        ZoomCheck();
    }

    void ZoomCheck()
    {
        isTouching1 = primaryFingerTouch.WasPerformedThisFrame();
        isTouching2 = secondaryFingerTouch.WasPerformedThisFrame();

        if (isTouching1 && isTouching2 && canStartZoom)
        {
            canStartZoom = false;
            StartCoroutine(Zoom());
        }

        if (!isTouching1 || !isTouching2)
        {
            StopAllCoroutines();
            canStartZoom = true;
        }
    }

    void Debug()
    {
        l.SetActive(isTouching1);
        r.SetActive(isTouching2);

        fovText.text = cam.fieldOfView.ToString();
        distanceText.text = distance.ToString();
    }

    //code from: https://www.youtube.com/watch?v=5LEVj3PLufE&t=274s
    IEnumerator Zoom()
    {
        float previousDistance = 0f;
        distance = 0f;

        while(true) 
        { 
            distance = Vector2.Distance(touchPos1,touchPos2);

            if(distance < minDistance)
            {
                previousDistance = distance;
                yield return null;
            }
            else if(distance > previousDistance) 
            {
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (zoomSpeed * Time.deltaTime), minFov, maxFov);
            }
            else if(distance < previousDistance) 
            {
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + (zoomSpeed * Time.deltaTime), minFov, maxFov);
            }

            previousDistance = distance;
            yield return null;
        }
    } 
}
