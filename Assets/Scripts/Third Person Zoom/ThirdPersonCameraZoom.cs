using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ThirdPersonCameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed, zoomInSeconds, minFov, maxFov;

    float zoomInput, zoomAmount;

    private void OnZoom(InputValue inputValue)
    {
        zoomInput = inputValue.Get<float>();

        if (zoomInput < 0) { zoomAmount = Mathf.Clamp(cam.fieldOfView + zoomSpeed, minFov, maxFov); }
        else if (zoomInput > 0) { zoomAmount = Mathf.Clamp(cam.fieldOfView - zoomSpeed, minFov, maxFov); }

        StartCoroutine(MoveOverSeconds(zoomAmount, zoomInSeconds));
    }

    //code from: https://answers.unity.com/questions/572851/way-to-move-object-over-time.html
    public IEnumerator MoveOverSeconds(float end, float seconds)
    {

        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cam.fieldOfView = end;
    }
}
