using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWidget : MonoBehaviour
{
    [SerializeField] private GameObject objectToNudge, buttons, northButton, eastButton, southButton, westButton, upButton, downButton, xZRotateButtons, yRotateButtons, uI;

    PlaceableObject placeableObject;

    private void Start()
    {
        EventManager.current.EnableMovementWidget += EnableMovementWidget;
        EventManager.current.DisableMovementWidget += DisableMovementWidget;
        EventManager.current.EnableObject += EnableBlock;
    }

    public void EnableMovementWidget(GameObject objectToMove)
    {
        EventManager.current.EventTrigger("SwitchUIState", uI);
        placeableObject = objectToMove.GetComponent<PlaceableObject>();
        objectToNudge = objectToMove;

        xZRotateButtons.SetActive(placeableObject.useXZRotateButtons);
        yRotateButtons.SetActive(placeableObject.useYRotateButtons);

        buttons.SetActive(true);
    }

    public void DisableMovementWidget()
    {
        buttons.SetActive(false);
    }

    public void EnableBlock()
    {
        placeableObject.DisableGhost();
    }
    public void NudgeNorth()
    {
        objectToNudge.transform.Translate(Vector3.forward);
    }

    public void NudgeEast()
    {
        objectToNudge.transform.Translate(Vector3.right);
    }

    public void NudgeSouth()
    {
        objectToNudge.transform.Translate(Vector3.back);
    }

    public void NudgeWest()
    {
        objectToNudge.transform.Translate(Vector3.left);
    }

    public void NudgeDown()
    {
        objectToNudge.transform.Translate(Vector3.down);
    }

    public void NudgeUp()
    {
        objectToNudge.transform.Translate(Vector3.up);
    }

    private void Update()
    {
        if (objectToNudge != null) { transform.position = objectToNudge.transform.position; }
        CheckForOccupiedVectors();
    }

    private void CheckForOccupiedVectors()
    {
        northButton.SetActive(!OccupiedVectorManager.current.VectorIsOccupied(transform.position + Vector3.forward));
        eastButton.SetActive(!OccupiedVectorManager.current.VectorIsOccupied(transform.position + Vector3.right));
        southButton.SetActive(!OccupiedVectorManager.current.VectorIsOccupied(transform.position + Vector3.back));
        westButton.SetActive(!OccupiedVectorManager.current.VectorIsOccupied(transform.position + Vector3.left));
        upButton.SetActive(!OccupiedVectorManager.current.VectorIsOccupied(transform.position + Vector3.up));
        downButton.SetActive(!OccupiedVectorManager.current.VectorIsOccupied(transform.position + Vector3.down));

    }
}
