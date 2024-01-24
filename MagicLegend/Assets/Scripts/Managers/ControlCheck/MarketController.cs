using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketController : MonoBehaviour
{
    private GameObject marketOverview;
    public GameObject skillBar;

    public void Start()
    {
        marketOverview = GameManager.instance.marketOverview;
        marketOverview.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            marketOverview.SetActive(true);
            skillBar.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            marketOverview.SetActive(false);
            skillBar.SetActive(true);
        }
    }
}
