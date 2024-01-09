using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ObjectsPool objectsPool;

    [Header("Player Settings")]
    public float playerSpeed = 5f;
    public DynamicJoystick joystick;
    public Rigidbody rb;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public TransitionSettings[] transitions;

    public bool isAvailableShoot;
    public bool startGame;
    public GameObject scrollHand;
    public float fireRate;

    public GameObject marketOverview;

    private void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
        MakeInstance();
        //DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        startGame = true;
        scrollHand.SetActive(false);
    }

    public void WinGame()
    {
        TransitionManager.Instance().Transition("RunPart", transitions[0], 2f);

    }

    public void GameOver()
    {

    }
}
