using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;
public class StartRunPart : MonoBehaviour
{
    public TransitionSettings[] transitions;

    private void Start()
    {
        transitions = GameManager.instance.transitions;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TransitionManager.Instance().Transition("RunPart", transitions[0], 0.5f);
        }
    }
}
