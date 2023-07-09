using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ConveyorBlock : MonoBehaviour
{
    [SerializeField] float conveyorSpeed;

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.GetComponent<Parcel>())
        {
            
            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
            float velocityInDirection = Vector3.Dot(rb.velocity, transform.forward);

            if(velocityInDirection < conveyorSpeed && rb.velocity.magnitude < conveyorSpeed) 
            {
                Vector3 forceToAdd = (conveyorSpeed - velocityInDirection) * transform.forward;
                Vector3.ClampMagnitude(forceToAdd, conveyorSpeed);

                rb.AddForce(forceToAdd, ForceMode.Impulse);
            }
        }
    }

}
