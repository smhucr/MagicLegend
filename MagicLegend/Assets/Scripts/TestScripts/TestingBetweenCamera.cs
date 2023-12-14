using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class TestingBetweenCamera : MonoBehaviour
{
    public GameObject joystick;
    public PlayerControl playerControl;
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;

    public void ChangeToLobby()
    {
        joystick.SetActive(true);
        playerControl.enabled = false;
        vCam1.Priority = 9;
        vCam2.Priority = 11;
    }

    public void ChangeToRun()
    {
        joystick.SetActive(false);
        playerControl.enabled = true;
        vCam1.Priority = 11;
        vCam2.Priority = 9;
    }
}
