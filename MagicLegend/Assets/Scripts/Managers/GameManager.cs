using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool isAvailableShoot;
    public bool startGame;
    public GameObject scrollHand;
    public float fireRate;

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
        SceneManager.LoadScene("RunPart");
    }
}
