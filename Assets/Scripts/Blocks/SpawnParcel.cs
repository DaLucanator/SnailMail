using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParcel : MonoBehaviour
{
    [SerializeField] GameObject parcel;
    [SerializeField] Transform spawnPos;
    [SerializeField] float spawnDelay;

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private void InstantiateParcel()
    {
        Instantiate(parcel, spawnPos.position, Quaternion.identity);
        StopAllCoroutines();

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(spawnDelay);
        InstantiateParcel();
    }
}
