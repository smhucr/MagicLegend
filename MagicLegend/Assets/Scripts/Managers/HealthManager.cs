using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public GameManager gameManager;
    public MainPlayer mainPlayer;

    public Sprite emptyHearth;
    public Sprite fullHearth;
    public Image[] hearths;

    private void Start()
    {
        gameManager = GameManager.instance;
        mainPlayer = gameManager.mainPlayer.GetComponent<MainPlayer>();
        health = mainPlayer.health;
        maxHealth = mainPlayer.maxHealth;
        InstantiateHearths();

    }

    public void InstantiateHearths()
    {
        health = mainPlayer.health;
        for (int i = 0; i < hearths.Length; i++)
        {

            if (i < health)
            {
                hearths[i].sprite = fullHearth;
            }
            else
            {
                hearths[i].sprite = emptyHearth;
            }
        }
    }


}
