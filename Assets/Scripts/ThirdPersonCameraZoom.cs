using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ThirdPersonCameraZoom : MonoBehaviour
{
    [SerializeField] private Transform anchor;
    [SerializeField] private float zoomAmount, zoomInSeconds;
    [SerializeField] private Transform cam;

    float zoomInput;
    Vector3 direction;

    private void OnZoom(InputValue inputValue)
    {
        zoomInput = inputValue.Get<float>();

        if(zoomInput > 0) { direction = Vector3.Normalize(anchor.position - cam.position); }
        else if (zoomInput < 0) { direction = Vector3.Normalize(cam.position - anchor.position); }

        Vector3 pointToMoveTo = cam.position + (direction * zoomAmount);

        StartCoroutine(MoveOverSeconds(pointToMoveTo, zoomInSeconds));
    }

    //code from: https://answers.unity.com/questions/572851/way-to-move-object-over-time.html
    public IEnumerator MoveOverSeconds(Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = cam.position;
        while (elapsedTime < seconds)
        {
            cam.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cam.position = end;
    }
}
