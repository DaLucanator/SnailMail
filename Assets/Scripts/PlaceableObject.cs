using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private GameObject ghost, actualBlock;
    public bool useYButtons, useXZRotateButtons, useYRotateButtons;
    public float objectSize;

    public void DisableGhost()
    {
        ghost.SetActive(false);
        actualBlock.SetActive(true);
    }
}
