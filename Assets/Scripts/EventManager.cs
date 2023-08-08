using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    void Awake()
    {
        current = this;
    }

    //Mobile Movement Events || Speak to MobileCameraRotate, MobileCameraTranslate, & MobilePinchZoom
    public event Action DisableMobileMovement;
    public event Action EnableMobileMovement;

    //Object Placer Events || Speak to ObjectPlacer
    public event Action<GameObject> StartObjectPlacement;
    public event Action StopObjectPlacement;           
    public event Action ConfirmObjectPlacement;
    public event Action PauseObjectPlacement;
    public event Action ContinueObjectPlacement;

    //Movement Widget Events || Speak to Movement Widget
    public event Action<GameObject> EnableMovementWidget;
    public event Action DisableMovementWidget;
    public event Action EnableObject;

    //UI Manager Events || Speak to UI Manager
    public event Action<GameObject> SwitchUIState;
    public event Action GoHome;

    //Placeable Room Events || Speak to PlaceableRoom(s)
    public event Action EnableRoomGrid;
    public event Action DisableRoomGrid;

    public Dictionary<string, Action> events = new Dictionary<string, Action>()
    {
        {"DisableMobileMovement", () => current.DisableMobileMovement() },
        {"EnableMobileMovement", () => current.EnableMobileMovement() },

        {"StopObjectPlacement", () => current.StopObjectPlacement() },
        {"ConfirmObjectPlacement", () => current.ConfirmObjectPlacement() },
        {"PauseObjectPlacement", () => current.PauseObjectPlacement() },
        {"ContinueObjectPlacement", () => current.ContinueObjectPlacement() },

        {"DisableMovementWidget", () => current.DisableMovementWidget() },
        {"EnableObject", () => current.EnableObject() },

        {"GoHome", () => current.GoHome() },

        {"EnableRoomGrid", () => current.EnableRoomGrid() },
        {"DisableRoomGrid", () => current.DisableRoomGrid() }
    };

    public Dictionary<string, Action<GameObject>> objectEvents = new Dictionary<string, Action<GameObject>>()
    {
        {"StartObjectPlacement", (GameObject) => current.StartObjectPlacement(GameObject) },

        {"EnableMovementWidget", (GameObject) => current.EnableMovementWidget(GameObject) },

        {"SwitchUIState", (GameObject) => current.SwitchUIState(GameObject) }
    };

    public void EventTrigger( String eventToTrigger )
    {
        events[eventToTrigger]();
    }

    public void EventTrigger( String eventToTrigger, GameObject relevantObject )
    {
        objectEvents[eventToTrigger](relevantObject);
    }

}
