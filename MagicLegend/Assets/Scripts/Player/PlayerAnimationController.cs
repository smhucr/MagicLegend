using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    GameManager gameManager;
    public AnimatorController animatorController;
    private MainPlayer mainPlayer;
    private void Start()
    {
        gameManager = GameManager.instance;
        mainPlayer = gameObject.GetComponent<MainPlayer>();
    }
    private void FixedUpdate()
    {
        if (mainPlayer.health <= 0)
        {
            mainPlayer.playerCurrentState = Player.PlayerState.Die;
        }
        if (gameManager.isGameOver && mainPlayer.playerCurrentState != Player.PlayerState.Die)
        {
            mainPlayer.playerCurrentState = Player.PlayerState.Idle;
            Idle();
        }
        else
        {
            switch (mainPlayer.playerCurrentState)
            {
                case Player.PlayerState.Idle:
                    Idle();
                    break;
                case Player.PlayerState.Walk:
                    Walk();
                    break;
                case Player.PlayerState.Die:
                    Die();
                    break;
            }
        }

    }

    private void Idle()
    {
        animatorController.animator.Play(animatorController.idleAnimation);
    }
    private void Walk()
    {
        animatorController.animator.Play(animatorController.chaseAnimation);
    }
    private void Die()
    {
        animatorController.animator.Play(animatorController.deathAnimation);
    }

}
