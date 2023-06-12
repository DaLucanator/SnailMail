using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MobilePinchZoom : MonoBehaviour
{
    [SerializeField] private Transform cam, anchor;
    [SerializeField] private float zoomAmount, zoomSpeed;

    bool isTouching1, isTouching2;
    Vector2 touchPos1,touchPos2;

    bool canStartZoom;

    void OnPrimaryTouchContact(InputValue inputValue)
    {
        isTouching1 = inputValue.Get<bool>();
    }

    void OnPrimarySecondaryContact(InputValue inputValue)
    {
        isTouching2 = inputValue.Get<bool>();
    }

    void OnPrimaryFingerTouch(InputValue inputValue)
    {
        touchPos1 = inputValue.Get<Vector2>();
    }

    void OnSecondaryFingerTouch(InputValue inputValue)
    {
        touchPos2 = inputValue.Get<Vector2>();
    }

    private void Update()
    {
        if(isTouching1 && isTouching2 && canStartZoom)
        {
            canStartZoom = false;
            StartCoroutine(Zoom());
        }

        if(!isTouching1 || !isTouching2) 
        { 
            StopAllCoroutines();
            canStartZoom = true;
        }
    }

    //code from: https://www.youtube.com/watch?v=5LEVj3PLufE&t=274s
    IEnumerator Zoom()
    {
        float previousDistance = 0f;
        float distance = 0f;

        while(true) 
        { 
            distance = Vector2.Distance(touchPos1,touchPos2);

            if(distance > previousDistance) 
            {
                Vector3 direction = Vector3.Normalize(anchor.position - cam.position);
                cam.position = Vector3.Slerp(cam.position, direction * zoomAmount, Time.deltaTime * zoomSpeed);
            }
            else if(distance < previousDistance) 
            {
                Vector3 direction = Vector3.Normalize(cam.position - anchor.position);
                cam.position = Vector3.Slerp(cam.position, direction * zoomAmount, Time.deltaTime * zoomSpeed);
            }

            previousDistance = distance;
            yield return null;
        }
    } 
}
