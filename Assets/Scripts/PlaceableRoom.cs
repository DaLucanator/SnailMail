using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableRoom : PlaceableObject
{
    [SerializeField] private GameObject fakeGrid;

    private void Start()
    {
        EventManager.current.EnableRoomGrid += EnableRoomGrid;
        EventManager.current.DisableRoomGrid += DisableRoomGrid;
    }

    private void EnableRoomGrid()
    {
        fakeGrid.SetActive(true);
    }

    private void DisableRoomGrid()
    {
        fakeGrid.SetActive(false);
    }
}
