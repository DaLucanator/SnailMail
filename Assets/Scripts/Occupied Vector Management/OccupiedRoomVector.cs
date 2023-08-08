using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedRoomVector : MonoBehaviour
{
    public void Start()
    {
        OccupiedVectorManager.current.OccupyRoomVector(transform.position);
    }
}
