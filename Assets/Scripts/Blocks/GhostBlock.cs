using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlock : MonoBehaviour
{
    [SerializeField] private Transform sendRayhere;

    void Start()
    {
        
    }

    void OnDrawGizmos()
    {

        Gizmos.DrawLine(transform.position, sendRayhere.position);
    }
}
