using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;
public class StartRunPart : MonoBehaviour
{
    public GameObject mainVirtualCamera;

    public TransitionSettings[] transitions;

    private void Start()
    {
        transitions = GameManager.instance.transitions;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GameManager.instance.DisableMoveable(0.2f));
            GameManager.instance.isGameOver = true;
            mainVirtualCamera.SetActive(false);
            TransitionManager.Instance().Transition("RunPart", transitions[0], 1.1f);
        }
    }

}
