using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            print("Collision with Wall");
            gameManager.HearthDecrease(3);
            gameManager.GameOver();
        }
        else if (other.CompareTag("SpinnedSpike"))
        {
            print("Collision with SpinnedSpike");
            gameManager.HearthDecrease(1);
            if (gameManager.playerHealth == 0)
                gameManager.GameOver();
        }
        else if (other.CompareTag("FlameThrower"))
        {
            print("Collision with Spike");
            gameManager.HearthDecrease(1);
            if (gameManager.playerHealth == 0)
                gameManager.GameOver();
        }
        else if (other.CompareTag("Teleport"))
        {
            print("Collision with Teleport");
            gameManager.StartBossPart();
            gameManager.isGameOver = true;
            gameManager.DisableMoveable(0.2f);
            
        }
    }
}
