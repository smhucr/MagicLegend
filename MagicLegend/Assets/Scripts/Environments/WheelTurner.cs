using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelTurner : MonoBehaviour
{
    [SerializeField]
    private Vector3 turnDirection;
    [SerializeField]
    private float turnDuration;
    private void Start()
    {
        transform.DOLocalRotate(turnDirection, turnDuration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
