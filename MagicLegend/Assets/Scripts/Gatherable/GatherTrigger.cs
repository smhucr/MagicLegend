using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTrigger : MonoBehaviour
{
    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Essence"))
        {
            print("Gathered Essence");
            gameManager.MoneyIncrease(25);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Treasure"))
        {
            print("Gathered Treasure");
            if (Random.Range(0, 20) == 5)
            {
                gameManager.MoneyIncrease(Random.Range(50, 250));
            }
            else
            {
                gameManager.UpgradeKitIncrease(1);
            }   
            other.gameObject.SetActive(false);

        }
        else if (other.CompareTag("UpgradeKit"))
        {
            print("Gathered Upgrade Kit");
            gameManager.UpgradeKitIncrease(1);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Hearth"))
        {
            print("Gathered Hearth");
            gameManager.HearthIncrease(1);
            other.gameObject.SetActive(false);
        }

    }
}
