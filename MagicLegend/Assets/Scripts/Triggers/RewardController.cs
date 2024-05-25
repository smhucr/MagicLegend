using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    private GameObject rewardOverview;
    public GameObject skillBar;

    public void Start()
    {
        rewardOverview = GameManager.instance.rewardManager.dailyRewardsPanel;
        rewardOverview.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rewardOverview.SetActive(true);
            skillBar.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rewardOverview.SetActive(false);
            skillBar.SetActive(true);
        }
    }
}
