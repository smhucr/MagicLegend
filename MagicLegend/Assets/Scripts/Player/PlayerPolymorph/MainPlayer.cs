using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : Player
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;

        gameManager.playerHealth = health;
        maxHealth = health + 1;
        damageMultiplier = gameManager.playerDamageMultiplier;
    }
}
