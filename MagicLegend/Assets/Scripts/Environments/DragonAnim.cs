using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnim : MonoBehaviour
{
    private AnimatorController animatorController;

    private void Start()
    {
        animatorController = GetComponent<AnimatorController>();
        Idle();
    }

    public void Idle()
    {
        animatorController.animator.Play(animatorController.idleAnimation);
    }
}
