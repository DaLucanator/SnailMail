using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlipperBlock : MonoBehaviour
{
    [SerializeField] private float angle = 45f, force = 10f, waitToTrigger;
    [SerializeField] private Transform aimAt;

    private bool canFlip = true;
    [SerializeField] List <Rigidbody> bodyList = new List<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Parcel>())
        {
            bodyList.Add(other.GetComponent<Rigidbody>());

            if(canFlip)
            {
                canFlip = false;
                StartCoroutine(Wait());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Parcel>()) { bodyList.Remove(other.GetComponent<Rigidbody>()); }
    }

    private void Fling()
    {
        foreach (Rigidbody rb in bodyList.ToList())
        {
            rb.velocity = Vector3.zero;

            Vector3 directionVector = (aimAt.position - transform.position).normalized;
            rb.AddForce(directionVector * force, ForceMode.Impulse);

            bodyList.Remove(rb);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitToTrigger);

        Fling();

        canFlip = true;
    }
}
