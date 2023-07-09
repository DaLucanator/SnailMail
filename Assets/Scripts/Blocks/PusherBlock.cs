using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherBlock : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float animInSeconds, waitToTrigger;

    private bool canTrigger = true;

    private void OnTriggerStay(Collider other)
    {
        if(canTrigger && other.GetComponent<Parcel>())
        {
            canTrigger = false;
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitToTrigger);

        StartCoroutine(WaitForAnim());

    }

    private IEnumerator WaitForAnim()
    {
        anim.SetBool("push", true);
        yield return new WaitForSeconds(animInSeconds);

        anim.SetBool("push", false);

        canTrigger = true;
    }
}
