using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    //this is my pooiest script but it's not that bad

    [SerializeField] private GameObject uIMobile;

    private bool canPlaceObject, canPlaceObjectMobile, objectIsRoom, isPaused;
    private GameObject objectToPlace;
    private float gridSize;
    private Vector3 pos;

    [SerializeField] private PlayerInput playerInput;
    private InputAction primaryFingerTouch;
    private Vector2 touchPos1;

    private void Awake()
    {
        primaryFingerTouch = playerInput.actions["PrimaryFingerTouch"];
    }

    private void Start()
    {
        EventManager.current.StartObjectPlacement += StartObjectPlacement;
        EventManager.current.StopObjectPlacement += StopObjectPlacement;
        EventManager.current.ConfirmObjectPlacement += ConfirmObjectPlacement;
        EventManager.current.PauseObjectPlacement += PauseObjectPlacement;
        EventManager.current.ContinueObjectPlacement += ContinueObjectPlacement;
    }

    private void OnEnable()
    {
        primaryFingerTouch.performed += PrimaryFingerTouch;
    }

    void PrimaryFingerTouch(InputAction.CallbackContext context)
    {
        touchPos1 = context.ReadValue<Vector2>();
    }

    public void PauseObjectPlacement()
    {
        isPaused = true;
    }

    public void ContinueObjectPlacement()
    {
        isPaused = false;
    }

    public void StopObjectPlacement()
    {
        canPlaceObject = false;
        canPlaceObjectMobile = false;
        objectIsRoom = false;
        EventManager.current.EventTrigger("DisableRoomGrid");
        EventManager.current.EventTrigger("DisableMovementWidget");
        Destroy(objectToPlace);
    }

    public void ConfirmObjectPlacement()
    {
        if (objectIsRoom && !OccupiedVectorManager.current.RoomVectorIsOccupied(objectToPlace.transform.position))
        {
            objectIsRoom = false;
            canPlaceObject = false;
            canPlaceObjectMobile = false;
            EventManager.current.EventTrigger("DisableRoomGrid");
            objectToPlace.GetComponent<PlaceableObject>().DisableGhost();
            EventManager.current.EventTrigger("GoHome");
        }

        else if(!objectIsRoom && !OccupiedVectorManager.current.VectorIsOccupied(objectToPlace.transform.position))
        {
            canPlaceObject = false;
            canPlaceObjectMobile = false;
            EventManager.current.EventTrigger("EnableMovementWidget", objectToPlace);
        }
    }

    public void StartObjectPlacement (GameObject placeMe)
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            EventManager.current.EventTrigger("SwitchUIState", uIMobile);
            canPlaceObjectMobile = true;
        }
        else
        {
            canPlaceObject = true;
        }

        objectToPlace = Instantiate(placeMe, transform.position, Quaternion.identity);
        if (objectToPlace.GetComponent<PlaceableRoom>()) 
        {
            EventManager.current.EventTrigger("EnableRoomGrid");
            objectIsRoom = true; 
        }
        gridSize = objectToPlace.GetComponent<PlaceableObject>().objectSize;
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
        if(isPaused){ }
        else if(canPlaceObject)
        {
            ObjectFollow(Mouse.current.position.ReadValue());
        }
        else if(canPlaceObjectMobile)
        {
            ObjectFollow(touchPos1);
        }


    }

    private void ObjectFollow(Vector2 vector)
    {

        Ray ray = Camera.main.ScreenPointToRay(vector);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            pos = hit.point;

            pos.x -= pos.x % gridSize;
            pos.y -= pos.y % gridSize;
            pos.z -= pos.z % gridSize;

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
            ConfirmObjectPlacement();
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
