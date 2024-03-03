using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainEnemy;

public class PlayerDetection : MonoBehaviour
{
    public AgressiveEnemy agressiveEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agressiveEnemy. enemyCurrentState = EnemyState.Chase;
        }
    }

}
