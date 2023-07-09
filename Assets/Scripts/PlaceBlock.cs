using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceBlock : MonoBehaviour
{
    private bool canPlaceBlock;
    private GameObject blockToPlace;
    private Vector3 pos;

    public void PlaceABlock (GameObject block)
    {
        blockToPlace = Instantiate(block,pos,Quaternion.identity);
        canPlaceBlock= true;
    }

    public void stopPlacingBlock()
    {
        canPlaceBlock= false;
    }

    private void FixedUpdate()
    {
        if(canPlaceBlock)
        {
            BlockFollowMouse();
        }
    }

    private void BlockFollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            pos = hit.point;

            pos.x -= pos.x % 1;
            pos.y -= pos.y % 1;
            pos.z -= pos.z % 1;

            pos.x += 0.5f;
            pos.y += 0.5f;
            pos.z += 0.5f;

            blockToPlace.transform.position = pos;
        }
    }
}
