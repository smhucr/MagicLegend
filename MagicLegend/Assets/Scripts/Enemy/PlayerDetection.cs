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
            float elementLevel = GameManager.instance.currentElementLevel;
            int damage = (int)(elementLevel * GameManager.instance.playerDamageMultiplier);
            mainEnemy.TakeDamage(damage);
            other.gameObject.SetActive(false);
        }
    }

}
