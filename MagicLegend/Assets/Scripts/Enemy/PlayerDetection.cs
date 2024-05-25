using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainEnemy;

public class PlayerDetection : MonoBehaviour
{
    public MainEnemy mainEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {

            if (mainEnemy.enemyCurrentState != EnemyState.Attack && mainEnemy.enemyCurrentState != EnemyState.Die)
                mainEnemy.enemyCurrentState = EnemyState.Chase;

            int damage = GameManager.instance.playerDamage;
            mainEnemy.TakeDamage(damage);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Blast"))
        {
            if (mainEnemy.enemyCurrentState != EnemyState.Attack && mainEnemy.enemyCurrentState != EnemyState.Die)
                mainEnemy.enemyCurrentState = EnemyState.Chase;
            int damage = GameManager.instance.playerDamage;
            mainEnemy.TakeDamage((int)(damage * 1.5f));
        }

    }

}
