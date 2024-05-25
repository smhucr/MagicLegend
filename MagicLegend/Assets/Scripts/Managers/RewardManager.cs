using NiobiumStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiobiumStudios;

public class RewardManager : MonoBehaviour
{
    public DailyRewards dailyRewards;
    private GameManager gameManager;

    public GameObject dailyRewardsPanel;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    void OnEnable()
    {
        dailyRewards.onClaimPrize += OnClaimPrizeDailyRewards;
    }
    void OnDisable()
    {
        dailyRewards.onClaimPrize -= OnClaimPrizeDailyRewards;
    }
    // this is your integration function. Can be on Start or simply a function to be called
    public void OnClaimPrizeDailyRewards(int day)
    {
        //This returns a Reward object
        Reward myReward = dailyRewards.GetReward(day);

        // And you can access any property
        print(myReward.unit); // This is your reward Unit name
        print(myReward.reward); // This is your reward count
        if(myReward.unit == "UpgradeKit")
        {
            if (myReward.reward == 0)
            {
                gameManager.UpgradeKitIncrease(1);
            }
            else
            {
                gameManager.UpgradeKitIncrease(myReward.reward);
            }
        }
        else if (myReward.unit == "Essence")
        {
            gameManager.MoneyIncrease(myReward.reward);
        }

    }
}


