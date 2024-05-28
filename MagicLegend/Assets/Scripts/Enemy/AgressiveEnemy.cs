using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveEnemy : MainEnemy
{
    private float distance;
    private void Start()
    {
        damageValue = 1;
        attackTime = 1f;
        linearHP = 1.3f;
        exponentialHP = 0.02f;
        health = CalculateHP(PlayerPrefs.GetInt("Level"),3,linearHP,exponentialHP);
        maxHealth = health;
        moveSpeed = 4f;
        follow_distance = 3.75f;
        playerComponentObject = GameManager.instance.mainPlayer.transform;
        playerFollowObject = GameManager.instance.playerParent.transform;
    }

    public override void Idle()
    {
        print("I am idling");
        // Enemy Idle Animation
        enemyAnimator.animator.Play(enemyAnimator.idleAnimation);


    }

    public override void Chase()
    {
        print("I am chasing");
        enemyAnimator.animator.Play(enemyAnimator.chaseAnimation);
        // Enemy Chase Player 
        distance = Vector3.Distance(transform.position, playerFollowObject.position);
        if (distance > follow_distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerFollowObject.position.x, transform.position.y, playerFollowObject.position.z), moveSpeed * Time.deltaTime);
            transform.LookAt(new Vector3(playerFollowObject.position.x, transform.position.y, playerFollowObject.position.z));
        }
        else
        {
            enemyCurrentState = EnemyState.Attack;
        }


    }

    public override void Attack()
    {

        if (isAttackable)
        {
            distance = Vector3.Distance(transform.position, playerFollowObject.position);
            if (distance > follow_distance)
            {
                enemyCurrentState = EnemyState.Chase;

            }
            else
            {
                print("I am attacking");
                //Enemy Attack Animation
                enemyAnimator.animator.Play(enemyAnimator.attackAnimation);
                Invoke("PlayChaseAnimation", 1f);
                StartCoroutine(WaitAttackForAnimation(0.6f));
                isAttackable = false;
                StartCoroutine(AttackCooldown());

            }
        }
    }

    public override void Die()
    {
        print("I am dying");
        //Enemy Die Animation After Die Animation Destroy the Object
        
        enemyAnimator.animator.Play(enemyAnimator.deathAnimation);
        gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
        

    }

}
