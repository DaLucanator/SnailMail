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

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            Vector3 pos = hit.point;

            pos.x -= pos.x % 1;
            pos.y -= pos.y % 1;
            pos.z -= pos.z % 1;

            pos.x += 2.5f;
            pos.y += 1.5f;
            pos.z += 0.5f;

            block.transform.position = pos;

            Debug.Log(pos);
        }
    }
}
