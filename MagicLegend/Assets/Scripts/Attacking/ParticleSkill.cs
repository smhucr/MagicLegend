using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSkill : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Dummy"))
        {
            other.transform.GetChild(0).GetComponent<Dummy>().TakingDamage();
        }
        else if (other.CompareTag("Enemy"))
        {
            MainEnemy mainEnemy = other.GetComponent<MainEnemy>();
            print("taking damage");
            int damage = 1 + (int)(GameManager.instance.currentElementLevel / 6); //Default 150 damage total veriyor
            mainEnemy.TakeDamage(damage);
            if (mainEnemy.enemyCurrentState != MainEnemy.EnemyState.Attack && mainEnemy.enemyCurrentState != MainEnemy.EnemyState.Die)
                mainEnemy.enemyCurrentState = MainEnemy.EnemyState.Chase;

        }
    }
}
