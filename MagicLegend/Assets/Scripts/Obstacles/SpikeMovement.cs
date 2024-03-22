using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikeMovement : MonoBehaviour
{
    private Vector3 offsetRotate;
    //Offset 90,0,360
    private void Start()
    {
        offsetRotate = new Vector3(90, 0, 360);
        // Spike Movement Animation
        transform.DOMoveX(-3, 3).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        
        transform.DORotate(offsetRotate, 1f,RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

    }
}
