using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyTransition;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("Manager")]
    public static GameManager instance;
    public ObjectsPool objectPool;
    [Header("UI")]
    public GameObject scrollHand;
    public GameObject marketOverview;
    public GameObject gameOverPanel;
    public GameObject winGamePanel;
    [Header("Money")]
    public int currentMoney;
    public int upgradeKitCount;
    public TextMeshProUGUI currentMoneyText;
    public TextMeshProUGUI upgradeKitCountText;
    [Header("FireRate")]
    public float fireRate;
    public int fireRateLevel;
    public int fireRateCost;
    public Button fireRateButton;
    public TextMeshProUGUI fireRateCostText;
    public TextMeshProUGUI fireRateUpgradeToText;
    [Header("ElementLevel")]
    public float elementLevel;
    public int currentElementLevel; // Use with PlayerPrefs
    public int elementLevelCost;
    public Button elementLevelButton;
    public TextMeshProUGUI elementLevelCostText;
    public TextMeshProUGUI elementLevelUpgradeToText;
    [Header("Player")]
    public GameObject playerParent; // Moving Player
    public GameObject mainPlayer; // Player who has script features
    public int playerHealth;
    public float playerDamageMultiplier;
    [Header("Player Settings")]
    public float playerSpeed = 5f;
    public DynamicJoystick joystick;
    public Rigidbody rb;
    [Header("Enemy")]
    public Transform closestEnemy;
    [Header("Dummy")]
    public GameObject dummy;
    [Header("VFX")]
    public int selectedElement;
    public int[] elementLevels; //fire frost poison lightning
    public GameObject[] projectiles;
    public GameObject[] explosions;
    public GameObject[] sphereBlasts;
    public GameObject[] magicRain;
    public GameObject[] auraCasters;
    public GameObject[] mergedAuraCircles;
    public GameObject currentMergedAura;
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    [Header("Transition")]
    public TransitionSettings[] transitions;
    [Header("ControlCheckers")]
    public bool isAvailableShoot;
    public bool startGame;
    public bool isGameOver;
    public bool isTransitionOver;
    public bool isMoveable;
    private void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
        MakeInstance();
        selectedElement = PlayerPrefs.GetInt("SelectedElement");
        if (selectedElement == 0)
            elementLevels[selectedElement] = PlayerPrefs.GetInt("FireElementLevel");
        else if (selectedElement == 1)
            elementLevels[selectedElement] = PlayerPrefs.GetInt("FrostElementLevel");
        else if (selectedElement == 2)
            elementLevels[selectedElement] = PlayerPrefs.GetInt("PoisonElementLevel");
        else if (selectedElement == 3)
            elementLevels[selectedElement] = PlayerPrefs.GetInt("LightningElementLevel");
        SetSkillPrefabs();
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeMergedAura(); // Changing Player's Aura
        StartCoroutine(TransitionChecker());
        currentMoney = PlayerPrefs.GetInt("CurrentMoney");
        currentMoneyText.text = "Essence: " + currentMoney.ToString();

        currentElementLevel = GetCurrentElementLevel();


    }

    public int GetCurrentElementLevel()
    {
        int currentElementLevel = elementLevels[selectedElement];

        return currentElementLevel;
    }

    public void SetCurrentElementLevel(int selectedElement, int currentElementLevel)
    {
        if (selectedElement == 0)
        {
            PlayerPrefs.SetInt("FireElementLevel", currentElementLevel);
            elementLevels[selectedElement] = PlayerPrefs.GetInt("FireElementLevel");

        }
        else if (selectedElement == 1)
        {

            PlayerPrefs.SetInt("FireElementLevel", currentElementLevel);
            elementLevels[selectedElement] = PlayerPrefs.GetInt("FireElementLevel");
        }
        else if (selectedElement == 2)
        {
            PlayerPrefs.SetInt("PoisonElementLevel", currentElementLevel);
            elementLevels[selectedElement] = PlayerPrefs.GetInt("PoisonElementLevel");

        }
        else if (selectedElement == 3)
        {
            PlayerPrefs.SetInt("LightningElementLevel", currentElementLevel);
            elementLevels[selectedElement] = PlayerPrefs.GetInt("LightningElementLevel");
        }

    }


    public void StartGame()
    {
        startGame = true;
        scrollHand.SetActive(false);
    }

    public void WinGame()
    {
        isGameOver = true;
        startGame = false;
        winGamePanel.SetActive(true);

    }

    public void GameOver()
    {
        isGameOver = true;
        startGame = false;
        gameOverPanel.SetActive(true);
    }

    public void FireRateIncreaser()
    {
        //Increase FireRate with essence and max 20 level
        if (fireRateLevel < 20 && currentMoney >= fireRateCost)
        {

            currentMoney -= fireRateCost;
            currentMoneyText.text = "Essence: " + currentMoney.ToString();
            PlayerPrefs.SetInt("CurrentMoney", currentMoney);


            fireRateCost = (int)(fireRateCost * 1.3f);
            fireRateCostText.text = fireRateCost.ToString();
            fireRateUpgradeToText.text = "Upgrade to " + (fireRateLevel + 1).ToString();
            fireRateLevel++;
            PlayerPrefs.SetInt("FireRateLevel", fireRateLevel);
            PlayerPrefs.SetInt("FireRateCost", fireRateCost);
            fireRate = fireRate - 0.1f;
        }
    }

    public void ElementLevelIncreaser()
    {

        //Increase element level with upgrade kit and max 20 level
        if (currentElementLevel < 20 && currentMoney >= elementLevelCost)
        {
            if (upgradeKitCount > 0)
            {
                upgradeKitCount--;
                upgradeKitCountText.text = upgradeKitCount.ToString();
                PlayerPrefs.SetInt("UpgradeKitCount", upgradeKitCount);
            }
            else
            {
                //current money minus and change money text
                currentMoney -= elementLevelCost;
                currentMoneyText.text = "Essence: " + currentMoney.ToString();
                PlayerPrefs.SetInt("CurrentMoney", currentMoney);

            }

            currentElementLevel++;
            elementLevelCost = (int)(elementLevelCost * 1.3f);
            elementLevelCostText.text = elementLevelCost.ToString();
            elementLevelUpgradeToText.text = "Upgrade to " + elementLevel.ToString();
            SetCurrentElementLevel(selectedElement, currentElementLevel);
            PlayerPrefs.SetInt("ElementLevelCost", elementLevelCost);
            elementLevel = elementLevel + 0.1f;
        }






    }



    public void SetSkillPrefabs()
    {

        objectPool.pools[0].objectPrefab = projectiles[selectedElement];
        objectPool.pools[1].objectPrefab = sphereBlasts[selectedElement];
        objectPool.pools[2].objectPrefab = magicRain[selectedElement];
        objectPool.pools[3].objectPrefab = auraCasters[selectedElement];
        objectPool.pools[4].objectPrefab = mergedAuraCircles[selectedElement];

    }
    IEnumerator TransitionChecker()
    {
        yield return new WaitForSeconds(1.4f);
        isTransitionOver = true;
    }

    public void ChangeMergedAura()
    {
        var mergedAura = objectPool.GetPooledObject(4);
        mergedAura.transform.parent = playerParent.transform;
        mergedAura.SetActive(true);
        currentMergedAura = mergedAura;
    }

    public void BackToMainMenu()
    {
        TransitionManager.Instance().Transition("LobbyScene", transitions[0], 0.5f);
    }


    public void RestartRunPart()
    {
        TransitionManager.Instance().Transition("RunPart", transitions[0], 0.5f);
    }


    public IEnumerator DisableMoveable(float duration)
    {
        yield return new WaitForSeconds(duration);
        isMoveable = false;
    }

}
