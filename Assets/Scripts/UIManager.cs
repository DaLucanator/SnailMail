using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] uIStates;
    [SerializeField] GameObject home;

    public void Start()
    {
        EventManager.current.SwitchUIState += SwitchUIState;
        EventManager.current.GoHome += GoHome;
    }
    public void SwitchUIState(GameObject uIToEnable)
    {
        foreach(GameObject uIState in uIStates) { uIState.SetActive(false); }

        uIToEnable.SetActive(true);
    }

    public void GoHome()
    {
        SwitchUIState(home);
    }

    //used for mobile
    public void StartObjectPlacement(GameObject objectToPlace)
    {
        EventManager.current.EventTrigger("DisableMobileMovement");
        EventManager.current.EventTrigger("StartObjectPlacement", objectToPlace);
    }

    //used for mobile
    public void ConfirmObjectPLacement()
    {
        EventManager.current.EventTrigger("EnableMobileMovement");
        EventManager.current.EventTrigger("ConfirmObjectPLacement");
    }

    public void StopObjectPlacement()
    {
        EventManager.current.EventTrigger("StopObjectPlacement");
    }

    public void DisableMobileMovement()
    {
        EventManager.current.EventTrigger("ContinueObjectPlacement");
        EventManager.current.EventTrigger("DisableMobileMovement");
    }

    public void EnableMobileMovement()
    {
        EventManager.current.EventTrigger("PauseObjectPlacement");
        EventManager.current.EventTrigger("EnableMobileMovement");
    }

    public void EnableMobileMovement2()
    {
        EventManager.current.EventTrigger("EnableMobileMovement");
    }

    public void DisableMovementWidget()
    {
        EventManager.current.EventTrigger("DisableMovementWidget");
    }

    public void EnableObject()
    {
        EventManager.current.EventTrigger("EnableObject");
    }

}
