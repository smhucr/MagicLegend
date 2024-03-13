using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dummy : MonoBehaviour
{
    private Vector3 startRotation;
    public GameObject dummyParent;

    private void Start()
    {
        startRotation = dummyParent.transform.rotation.eulerAngles;
    }
    public void TakingDamage()
    {
        print("I am taking damage");
        // Enemy Taking Damage Animation
        //Random Number for Punch Rotation
        Vector3 punchRotation = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
        dummyParent.transform.DOPunchRotation(punchRotation, 0.7f, 10, 1).OnComplete(() => dummyParent.transform.DORotate(startRotation,0.3f));
    }


   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakingDamage();
        }
    }
}
