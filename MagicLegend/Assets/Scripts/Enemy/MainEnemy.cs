using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainEnemy : MonoBehaviour
{
    private GameManager gameManager;
    [Header("MainPlayer")]
    public Transform playerComponentObject;
    public Transform playerFollowObject;
    [Header("EnemyFeatures")]
    [SerializeField]
    public float health;
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public int damageValue;
    [SerializeField]
    public float follow_distance;
    [SerializeField]
    public float attackTime;
    [SerializeField]
    public bool isAttackable;
    [Header("Animation")]
    public AnimatorController enemyAnimator;


    //State Machine
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Die
    }
    public EnemyState enemyCurrentState = EnemyState.Idle;

    private void Awake()
    {
        maxHealth = health;
        playerComponentObject = GameManager.instance.mainPlayer.transform;
        isAttackable = true;
        gameManager = GameManager.instance;
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameOver)
        {
            enemyCurrentState = EnemyState.Idle;
            Idle();
        }
        else
        {
            if (health <= 0)
            {

                enemyCurrentState = EnemyState.Die;
            }

            switch (enemyCurrentState)
            {
                case EnemyState.Idle:
                    Idle();
                    break;
                case EnemyState.Chase:
                    Chase();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Die:
                    Die();
                    break;
            }
        }


    }

    public abstract void Idle();
    public abstract void Chase();
    public abstract void Attack();
    public abstract void Die();

    public IEnumerator AttackCooldown()
    {
        isAttackable = false;
        yield return new WaitForSeconds(attackTime);
        isAttackable = true;
    }

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
            Die();
    }


}
