using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class MovementWidget : MonoBehaviour
{
    [SerializeField] private GameObject objectToNudge, buttons, xZButtons, yButtons, rotateButtons, UI;

    private PlaceableObject placeableObject;
    
    public void EnableWidget(GameObject objectToMove)
    {
        placeableObject = objectToMove.GetComponent<PlaceableObject>();
        objectToNudge = objectToMove;

        xZButtons.SetActive(placeableObject.useXZButtons);
        yButtons.SetActive(placeableObject.useYButtons);
        rotateButtons.SetActive(placeableObject.useRotateButtons);

        buttons.SetActive(true);
        UI.SetActive(true);
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
    }
}
