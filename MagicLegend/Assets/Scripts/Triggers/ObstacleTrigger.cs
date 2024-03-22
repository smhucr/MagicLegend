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
            gameManager.GameOver();
        }
        else if (other.CompareTag("SpinnedSpike"))
        {
            print("Collision with SpinnedSpike");
            gameManager.playerHealth -= 1;
            if (gameManager.playerHealth == 0)
                gameManager.GameOver();
        }
        else if (other.CompareTag("Spike"))
        {
            print("Collision with Spike");
            gameManager.playerHealth -= 1;
            if (gameManager.playerHealth == 0)
                gameManager.GameOver();
        }
    }
}
