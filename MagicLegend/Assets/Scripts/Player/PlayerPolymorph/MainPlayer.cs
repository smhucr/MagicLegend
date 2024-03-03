using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : Player
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;

        health = gameManager.playerHealth;
        damageMultiplier = gameManager.playerDamageMultiplier;
    }
}
