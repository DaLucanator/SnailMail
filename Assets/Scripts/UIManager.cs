using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] uIStates;
    [SerializeField] ObjectPlacer objectPlacer;

    public void SwitchUIState(GameObject uIToEnable)
    {
        foreach(GameObject uIState in uIStates) { uIState.SetActive(false); }

        uIToEnable.SetActive(true);
    }

    public void PlaceObject(GameObject objectToPlace)
    {
        objectPlacer.PlaceObject(objectToPlace);
    }
}
