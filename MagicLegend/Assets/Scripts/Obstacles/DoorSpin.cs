using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorSpin : MonoBehaviour
{
    private void Start()
    {
        // Door Spin Animation
        transform.DOLocalRotate(new Vector3(0, 90, 0), 0.75f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
    }
}
