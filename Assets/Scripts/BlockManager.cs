using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockManager : MonoBehaviour
{
    public GameObject block;
    public void BlockFollowMouse()
    {

    }

    public void PlaceBlock(GameObject blockToPlace)
    {
        GameObject.Instantiate(blockToPlace);
    }

    public void NudgeNorth(GameObject blockToNudge)
    {
        blockToNudge.transform.Translate(Vector3.right);
    }

    public void NudgeDown(GameObject blockToNudge)
    {
        blockToNudge.transform.Translate(Vector3.down);
    }

    public void NudgeUp(GameObject blockToNudge)
    {
        blockToNudge.transform.Translate(Vector3.up);
    }

    public void RotateBlock(GameObject blockToRotate, Quaternion rotation)
    {

    }
}
