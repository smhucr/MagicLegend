using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private void FixedUpdate()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
