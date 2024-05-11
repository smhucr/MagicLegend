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
            other.gameObject.SetActive(false);
        }
    }

}
