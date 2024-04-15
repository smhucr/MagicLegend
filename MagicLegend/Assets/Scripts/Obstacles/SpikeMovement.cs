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
        float random = Random.Range(1.5f, 4.5f);
        offsetRotate = new Vector3(90, 0, 360);
        // Spike Movement Animation
        transform.DOMoveX(-3, random).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        transform.DORotate(offsetRotate, random,RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

    }
}
