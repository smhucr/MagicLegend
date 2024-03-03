using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;
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
    [Header("FireRate")]
    public float fireRate;
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
        SetSkillPrefabs();
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeMergedAura(); // Changing Player's Aura
        StartCoroutine(TransitionChecker());
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
