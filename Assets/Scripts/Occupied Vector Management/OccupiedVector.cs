using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedVector : MonoBehaviour
{
    public void Start()
    {
        OccupiedVectorManager.current.OccupyVector(transform.position);
    }
}
