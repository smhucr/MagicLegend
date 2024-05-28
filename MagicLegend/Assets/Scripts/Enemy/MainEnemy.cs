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
    [SerializeField]
    public float linearHP;
    [SerializeField]
    public float exponentialHP;
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
        if (health <= 0)
        {

            enemyCurrentState = EnemyState.Die;
        }
        if (gameManager.isGameOver && enemyCurrentState != EnemyState.Die)
        {
            enemyCurrentState = EnemyState.Idle;
            Idle();
        }
        else
        {
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
    public IEnumerator WaitAttackForAnimation(float timer)
    {
        yield return new WaitForSeconds(timer);
        playerComponentObject.GetComponent<MainPlayer>().TakeDamage(damageValue);
    }
    public void PlayIdleAnimation()
    {
        enemyCurrentState = EnemyState.Idle;
    }
    public void PlayChaseAnimation()
    {
        enemyCurrentState = EnemyState.Chase;
    }
    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0 && !gameManager.isGameOver)
        {
            Die();
            if (Random.Range(0, 20) != 5)
            {
                GameManager.instance.MoneyIncrease(Random.Range(25, 150));
            }
            else
            {
                GameManager.instance.UpgradeKitIncrease(1);
            }
        }
    }

    protected int CalculateHP(int level, int baseHP, float linearHP, float exponentialHP)
    {
        int hp = (int)(baseHP + (linearHP * level) * Mathf.Pow(1 + exponentialHP, level));
        return hp;
    }


}
