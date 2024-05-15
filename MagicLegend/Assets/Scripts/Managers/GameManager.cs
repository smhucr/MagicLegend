using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyTransition;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{
    [Header("Manager")]
    public static GameManager instance;
    public ObjectsPool objectPool;
    public HealthManager healthManager;
    public MarketManager marketManager;
    [Header("LevelState")]
    public SceneState currentSceneState;
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
    //0: Fire, 1: Frost, 2: Poison, 3: Lightning
    public int[] elementLevels;
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
        ChangeSceneState();
        elementLevels = new int[4];
        Application.targetFrameRate = 60;
        MakeInstance();
        //Player Features
        playerDamageMultiplier = 1.2f;
        currentMoney = PlayerPrefs.GetInt("CurrentMoney");
        currentMoneyText.text = "Essence: " + currentMoney.ToString();
        upgradeKitCount = PlayerPrefs.GetInt("UpgradeKitCount");
        upgradeKitCountText.text = "Upgrade Kit: " + upgradeKitCount.ToString();
        //Assign Element Levels
        selectedElement = PlayerPrefs.GetInt("SelectedElement");

        if (selectedElement == 0)
            elementLevels[selectedElement] = PlayerPrefs.GetInt("FireElementLevel");
        else if (selectedElement == 1)
            elementLevels[selectedElement] = PlayerPrefs.GetInt("FrostElementLevel");
        else if (selectedElement == 2)
            elementLevels[selectedElement] = PlayerPrefs.GetInt("PoisonElementLevel");
        else if (selectedElement == 3)
            elementLevels[selectedElement] = PlayerPrefs.GetInt("LightningElementLevel");

        if (currentSceneState == SceneState.Lobby)
        {
            elementLevelCost = PlayerPrefs.GetInt("ElementLevelCost" + selectedElement);
            if (elementLevelCost == 0)
            {
                elementLevelCost = 100;
                PlayerPrefs.SetInt("ElementLevelCost" + selectedElement, elementLevelCost);
            }
        }



        currentElementLevel = GetCurrentElementLevel();
        if (currentElementLevel == 0)
        {
            currentElementLevel = 1;
            SetCurrentElementLevel(selectedElement, currentElementLevel);
        }

        if (currentSceneState == SceneState.Lobby)
        {
            if (elementLevels[selectedElement] >= 20)
            {
                elementLevelUpgradeToText.text = "Max Level";
                elementLevelCostText.text = "Max Level";
                elementLevelButton.interactable = false;
            }
            else
            {
                elementLevelUpgradeToText.text = "Upgrade to " + (elementLevels[selectedElement] + 1).ToString();
                elementLevelCostText.text = elementLevelCost.ToString();
            }
        }


        //Assign FireRate
        fireRateLevel = PlayerPrefs.GetInt("FireRateLevel");
        fireRate = PlayerPrefs.GetFloat("FireRate");
        if (fireRate == 0)
        {
            fireRate = 2.5f;
            PlayerPrefs.SetFloat("FireRate", fireRate);
        }
        if (fireRateLevel == 0)
        {
            fireRateLevel = 1;
            PlayerPrefs.SetInt("FireRateLevel", fireRateLevel);
        }
        fireRateCost = PlayerPrefs.GetInt("FireRateCost");
        if (fireRateCost == 0)
        {
            fireRateCost = 100;
            PlayerPrefs.SetInt("FireRateCost", fireRateCost);
        }

        if (currentSceneState == SceneState.Lobby)
        {
            if (fireRateLevel >= 20)
            {
                fireRateUpgradeToText.text = "Max Level";
                fireRateCostText.text = "Max Level";
                fireRateButton.interactable = false;
            }
            else
            {
                fireRateUpgradeToText.text = "Upgrade to " + (fireRateLevel + 1).ToString();
                fireRateCostText.text = fireRateCost.ToString();
            }
        }


        //Skill Prefabs
        SetSkillPrefabs();
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeMergedAura(); // Changing Player's Aura
        StartCoroutine(TransitionChecker());

        print("ElementLevelCost" + selectedElement);

    }

    public void ChangeSceneState()
    {
        if (SceneManager.GetActiveScene().name == "LobbyScene")
        {
            currentSceneState = SceneState.Lobby;
        }
        else if (SceneManager.GetActiveScene().name == "RunPart")
        {
            currentSceneState = SceneState.RunPart;
        }
        else if (SceneManager.GetActiveScene().name == "BossArea")
        {
            currentSceneState = SceneState.BossPart;
        }


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


    public void MoneyIncrease(int incomeValue)
    {
        currentMoney += incomeValue;
        currentMoneyText.text = "Essence: " + currentMoney.ToString();
        PlayerPrefs.SetInt("CurrentMoney", currentMoney);
    }

    //money decrease
    public void MoneyDecrease(int decreaseValue)
    {
        currentMoney -= decreaseValue;
        currentMoneyText.text = "Essence: " + currentMoney.ToString();
        PlayerPrefs.SetInt("CurrentMoney", currentMoney);
    }

    //upgrade kit increase
    public void UpgradeKitIncrease(int incomeValue)
    {
        upgradeKitCount += incomeValue;
        if (upgradeKitCountText != null)
            upgradeKitCountText.text = "Upgrade Kit: " + upgradeKitCount.ToString();
        PlayerPrefs.SetInt("UpgradeKitCount", upgradeKitCount);
    }

    //upgrade kit decrease
    public void UpgradeKitDecrease(int decreaseValue)
    {
        upgradeKitCount -= decreaseValue;
        if (upgradeKitCountText != null)
            upgradeKitCountText.text = "Upgrade Kit: " + upgradeKitCount.ToString();
        PlayerPrefs.SetInt("UpgradeKitCount", upgradeKitCount);
    }

    //increase health
    public void HearthIncrease(int incomeValue)
    {
        MainPlayer cachedMainPlayer = mainPlayer.GetComponent<MainPlayer>();
        cachedMainPlayer.health += incomeValue;
        cachedMainPlayer.health = Mathf.Clamp(cachedMainPlayer.health, 0, 3);
        playerHealth = cachedMainPlayer.health;
        healthManager.InstantiateHearths();
    }
    //decrease health
    public void HearthDecrease(int decreaseValue)
    {
        MainPlayer cachedMainPlayer = mainPlayer.GetComponent<MainPlayer>();
        cachedMainPlayer.health -= decreaseValue;
        cachedMainPlayer.health = Mathf.Clamp(cachedMainPlayer.health, 0, 3);
        playerHealth = cachedMainPlayer.health;
        healthManager.InstantiateHearths();
    }

    public void FireRateIncreaser()
    {
        //Increase FireRate with essence and max 20 level
        if (fireRateLevel < 20 && currentMoney >= fireRateCost)
        {
            //current money minus and change money text
            MoneyDecrease(fireRateCost);


            fireRateCost = (int)(fireRateCost * 1.3f);
            fireRateCostText.text = fireRateCost.ToString();
            fireRateUpgradeToText.text = "Upgrade to " + (fireRateLevel + 2).ToString();
            fireRateLevel++;
            fireRate = fireRate - 0.1f;
            PlayerPrefs.SetInt("FireRateLevel", fireRateLevel);
            PlayerPrefs.SetInt("FireRateCost", fireRateCost);
            PlayerPrefs.SetFloat("FireRate", fireRate);

        }
        if (fireRateLevel >= 20)
        {
            fireRateUpgradeToText.text = "Max Level";
            fireRateCostText.text = "Max Level";
            fireRateButton.interactable = false;
        }
    }

    public void ElementLevelIncreaser()
    {

        //Increase element level with upgrade kit and max 20 level
        if (currentElementLevel < 20 && currentMoney >= elementLevelCost)
        {
            if (upgradeKitCount > 0)
            {
                UpgradeKitDecrease(1);
            }
            else
            {
                //current money minus and change money text
                MoneyDecrease(elementLevelCost);
            }
            if (elementLevelCost == 0)
                elementLevelCost = 100;
            currentElementLevel++;
            elementLevelCost = (int)(elementLevelCost * 1.3f);
            elementLevelCostText.text = elementLevelCost.ToString();
            elementLevelUpgradeToText.text = "Upgrade to " + (currentElementLevel + 1).ToString();
            SetCurrentElementLevel(selectedElement, currentElementLevel);
            PlayerPrefs.SetInt("ElementLevelCost" + selectedElement, elementLevelCost);
            PlayerPrefs.SetInt("ElementLevelCost", elementLevelCost);
        }
        if (currentElementLevel >= 20)
        {
            elementLevelUpgradeToText.text = "Max Level";
            elementLevelCostText.text = "Max Level";
            elementLevelButton.interactable = false;
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

    public void ChangeElement()
    {
        TransitionManager.Instance().Transition("LobbyScene", transitions[0], 0.5f);

        PlayerPrefs.SetInt("SelectedElement", marketManager.selectedElementIndex);
    }

    public void BackToMainMenu()
    {
        TransitionManager.Instance().Transition("LobbyScene", transitions[0], 0.5f);
    }



    public void RestartRunPart()
    {
        TransitionManager.Instance().Transition("RunPart", transitions[0], 0.5f);
    }

    public void StartBossPart()
    {
        TransitionManager.Instance().Transition("BossArea", transitions[0], 0.5f);
    }


    public IEnumerator DisableMoveable(float duration)
    {
        yield return new WaitForSeconds(duration);
        isMoveable = false;
    }


    public enum SceneState
    {
        Lobby,
        RunPart,
        BossPart
    }

}
