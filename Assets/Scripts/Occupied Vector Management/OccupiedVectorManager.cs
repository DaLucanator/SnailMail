using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OccupiedVectorManager : MonoBehaviour
{
    public static OccupiedVectorManager current;

    [SerializeField] private List<Vector3> occupiedVectors = new List<Vector3>();
    [SerializeField] private List<Vector3> occupiedRoomVectors = new List<Vector3>();

    void Awake()
    {
        current = this;
    }

    public void OccupyVector(Vector3 vectorToAdd)
    {
        if (!VectorIsOccupied(vectorToAdd)) { occupiedVectors.Add(vectorToAdd); }

        else { Debug.Log("ERROR: The Vector you are trying to add: " + vectorToAdd.ToString() + " is already occupied. Something has gone wrong"); }
    }

    public void UnoccupyVector(Vector3 vectorToRemove)
    {
        if (VectorIsOccupied(vectorToRemove)) { occupiedVectors.Remove(vectorToRemove); }

        else { Debug.Log("ERROR: The Vector you are trying to remove: " + vectorToRemove.ToString() + " is not occupied. Something has gone wrong"); }
    }

    public void OccupyRoomVector(Vector3 vectorToAdd)
    {
        if (!RoomVectorIsOccupied(vectorToAdd)) { occupiedRoomVectors.Add(vectorToAdd); }

        else { Debug.Log("ERROR: The Room Vector you are trying to add: " + vectorToAdd.ToString() + " is already occupied. Something has gone wrong"); }
    }

    public void UnoccupyRoomVector(Vector3 vectorToRemove)
    {
        if (RoomVectorIsOccupied(vectorToRemove)) { occupiedRoomVectors.Remove(vectorToRemove); }

        else { Debug.Log("ERROR: The Vector you are trying to remove: " + vectorToRemove.ToString() + " is not occupied. Something has gone wrong"); }
    }

    public bool VectorIsOccupied(Vector3 vectorToCheck)
    {
        if (occupiedVectors.Contains(vectorToCheck)) { return true; }
        else return false;
    }

    public bool RoomVectorIsOccupied(Vector3 vectorToCheck)
    {
        if (occupiedRoomVectors.Contains(vectorToCheck)) { return true; }
        else return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 vector in occupiedVectors)
        {
            Gizmos.DrawCube(vector, Vector3.one);
        }
    }
}
