using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private MovementWidget movementWidget;
    [SerializeField] private GameObject UI;

    private bool canPlaceObject;
    private GameObject objectToPlace;
    private Vector3 pos;

    public void PlaceAnObject (GameObject placeMe)
    {
        objectToPlace = null;
        canPlaceObject = true;
        objectToPlace = Instantiate(placeMe, pos, Quaternion.identity);
    }

    public void stopPlacingObject()
    {
        canPlaceObject = false;
        Destroy(objectToPlace);
        UI.SetActive(false);
    }

    private void OnFire()
    {
        if(canPlaceObject)
        {
            StartCoroutine(CheckForExitButton());
        }
    }

    private void FixedUpdate()
    {
        if(canPlaceObject)
        {
            ObjectFollowMouse();
        }
    }

    private void ObjectFollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            pos = hit.point;

            pos.x -= pos.x % 1;
            pos.y -= pos.y % 1;
            pos.z -= pos.z % 1;

            pos.x += 0.5f;
            pos.y += 0.5f;
            pos.z += 0.5f;

            objectToPlace.transform.position = pos;
        }
    }

    private IEnumerator CheckForExitButton()
    {
        yield return new WaitForSeconds(0.1f);

        if (canPlaceObject)
        {
            movementWidget.EnableWidget(objectToPlace);
            canPlaceObject = false;
            UI.SetActive(false);
        }
    }

    IEnumerator RotateObject(GameObject objectToRotate, float angle, Vector3 axis, float inTime)
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

            objectToRotate.transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

            yield return null;
        }
    }
}
