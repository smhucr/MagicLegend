using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Settings")]
    public float playerSpeed = 5f;
    public DynamicJoystick joystick; 
    public Rigidbody rb;

    private void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }
    private void Awake()
    {
        MakeInstance();
        //DontDestroyOnLoad(gameObject);
    }
}
