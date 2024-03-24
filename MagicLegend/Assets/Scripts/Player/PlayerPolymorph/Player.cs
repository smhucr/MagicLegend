using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [Header("CharacterFeatures")]
    [SerializeField]
    public int health;
    [SerializeField]
    public int maxHealth;
    [SerializeField]
    public float damageMultiplier;

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
        health = Mathf.Clamp(health, 0, 10);
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        print("I'm Dead Bruah");
    }

    public void Reborn()
    {
        //This is a ADS part
    }
}
