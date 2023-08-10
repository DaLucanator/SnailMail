using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private GameObject ghost, actualBlock;
    public bool useYButtons, useXZRotateButtons, useYRotateButtons;
    public float objectSize;

    [SerializeField]private Vector3[] vertices = new Vector3[8];
    private Vector3 size;

    public void DisableGhost()
    {
        ghost.SetActive(false);
        actualBlock.SetActive(true);
    }

    // code from https://gamedev.stackexchange.com/questions/128833/how-can-i-get-a-box-colliders-corners-vertices-positions
    public void GetColliderVertexPositions()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        vertices[0] = transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f);
        vertices[1] = transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f);
        vertices[2] = transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f);
        vertices[3] = transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f);
        vertices[4] = transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, -b.size.z) * 0.5f);
        vertices[5] = transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, -b.size.z) * 0.5f);
        vertices[6] = transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, b.size.z) * 0.5f);
        vertices[7] = transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, b.size.z) * 0.5f);
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] verticesInt = new Vector3Int[7];

        for(int i = 0; i < vertices.Length; i++)
        {
            GridLayout myGridLayout = GridManager.current.ReturnBlockGridLayer(vertices[i]);
            verticesInt[i] = myGridLayout.WorldToCell(vertices[i]);
        }
    }

    //find the 4 lowest vertices
    //find the center of the vertices
    //find the center of the other vertices
    //difference between them is the height

    private void Update()
    {
        GetColliderVertexPositions();
    }
}
