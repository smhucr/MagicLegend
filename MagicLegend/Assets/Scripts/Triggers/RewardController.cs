using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    private GameObject rewardOverview;
    private GameObject rewardCanvas;
    public GameObject skillBar;

    public void Start()
    {
        rewardOverview = GameManager.instance.rewardManager.dailyRewardsPanel;
        rewardCanvas = GameManager.instance.rewardCanvas;
        rewardOverview.SetActive(false);
        rewardCanvas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rewardOverview.SetActive(true);
            rewardCanvas.SetActive(true);
            skillBar.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rewardOverview.SetActive(false);
            rewardCanvas.SetActive(true);
            skillBar.SetActive(true);
        }
    }
}
