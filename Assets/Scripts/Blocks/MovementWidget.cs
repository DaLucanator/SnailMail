using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWidget : MonoBehaviour
{
    public void NudgeNorth(GameObject blockToNudge)
    {
        blockToNudge.transform.Translate(Vector3.back);
    }

    public void NudgeDown(GameObject blockToNudge)
    {
        blockToNudge.transform.Translate(Vector3.down);
    }

    public void NudgeUp(GameObject blockToNudge)
    {
        blockToNudge.transform.Translate(Vector3.up);
    }
}
