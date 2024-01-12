using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    private GameManager gameManager;
    public float RunSpeed;
    public float DesiredX;
    public float ClampX;
    public bool levelEndingWorks;
    
    public Transform player;

    public Transform mainPlayer
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
            mainPlayer.tag = "Player";
        }
    }



    private void Start()
    {
        gameManager = GameManager.instance; //Cached
    }
    private void Update()
    {
        if (gameManager.startGame & !gameManager.isGameOver)
        {
            HandleSwerve();

            if (!levelEndingWorks)
                HandleControll();

        }

    }

    void HandleSwerve()
    {
        Vector3 pos = mainPlayer.position;
        pos.x = Mathf.Clamp(DesiredX, ClampX * -1, ClampX);
        mainPlayer.position = Vector3.Lerp(mainPlayer.position, pos, 3);
    }

    void HandleControll()
    {
        transform.position += (Vector3.forward * RunSpeed * Time.deltaTime);
    }


}
