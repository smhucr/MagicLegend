using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MainEnemy
{
    private float distance;
    private void Start()
    {
        damageValue = 1;
        attackTime = 2f;
        linearHP = 15f;
        exponentialHP = 0.025f;
        health = CalculateHP(PlayerPrefs.GetInt("Level"), 100, linearHP, exponentialHP);
        maxHealth = health;
        moveSpeed = 4f;
        follow_distance = 2f;
        playerComponentObject = GameManager.instance.mainPlayer.transform;
        playerFollowObject = GameManager.instance.playerParent.transform;
    }

    public override void Idle()
    {
        print("I am idling");
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
        // Change To Idle State code Write Here
        //else if
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
                Invoke("PlayChaseAnimation", 1.85f);
                StartCoroutine(WaitAttackForAnimation(0.8f));
                isAttackable = false;
                StartCoroutine(AttackCooldown());

            }
        }
    }

    public override void Die()
    {
        if (GameManager.instance.isGameOver)
        {
            print("I am dying");
            enemyAnimator.animator.Play(enemyAnimator.deathAnimation);
            //Enemy Die Animation After Die Animation Destroy the Object    
            gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            GameManager.instance.WinGame();
        }


    }
}
