using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSkill : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Dummy"))
        {
            other.transform.GetChild(0).GetComponent<Dummy>().TakingDamage();
        }
        else if (other.CompareTag("Enemy"))
        {
            print("taking damage");
        }
    }
}
