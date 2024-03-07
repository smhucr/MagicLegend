using DG.Tweening;
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
        health = 5f;
        maxHealth = health;
        moveSpeed = 4f;
        follow_distance = 1f;
        playerComponentObject = GameManager.instance.mainPlayer.transform;
        playerFollowObject = GameManager.instance.playerParent.transform;
    }

    public override void Idle()
    {
        print("I am idling");
        // Enemy Idle Animation


    }

    public override void Chase()
    {
        print("I am chasing");
        // Enemy Chase Player 
        distance = Vector3.Distance(transform.position, playerFollowObject.position);
        if (distance > follow_distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerFollowObject.position.x,transform.position.y,playerFollowObject.position.z), moveSpeed * Time.deltaTime);
        }
        else
        {
            enemyCurrentState = EnemyState.Attack;
        }
        
            
    }

    public override void Attack()
    {
        print("I am attacking");
        if (isAttackable)
        {
            //Enemy Attack Animation
            playerComponentObject.GetComponent<MainPlayer>().TakeDamage(damageValue);
            distance = Vector3.Distance(transform.position, playerFollowObject.position);
            if (distance > follow_distance)
            {
                enemyCurrentState = EnemyState.Chase;
            }

            StartCoroutine(AttackCooldown());
        }
    }

    public override void Die()
    {
        print("I am dying");
        //Enemy Die Animation After Die Animation Destroy the Object    


    }
}
