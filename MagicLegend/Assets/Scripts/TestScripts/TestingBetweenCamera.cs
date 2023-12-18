using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;
public class TestingBetweenCamera : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    
    
    IEnumerator FPSCor()
    {
        fpsText.text = ("FPS: "+ 1 / Time.unscaledDeltaTime );
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(FPSCor());

    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(FPSCor());
    }

    public void ChangeToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void ChangeToRun()
    {
        SceneManager.LoadScene("RunPart");
    }
}
