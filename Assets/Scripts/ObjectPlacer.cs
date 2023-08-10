using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject uIMobile;

    private bool canPlaceObject, canPlaceObjectMobile, objectIsRoom, isPaused;
    private GameObject objectToPlace;
    private float gridSize;
    private Vector3 pos;

    [SerializeField] private PlayerInput playerInput;
    private InputAction primaryFingerTouch;
    private Vector2 touchPos1;

    private void BeginObjectPlacement(GameObject placeMe)
    {

    }

    private void PauseObjectPlacement()
    {

    }

    private void CancelObjectPlacement()
    {

    }

    private void CompleteObjectPlacement()
    {

    }
}
