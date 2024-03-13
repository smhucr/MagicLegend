using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestEnemy : MonoBehaviour
{
    [SerializeField]
    protected float targettingInterval = 0.2f; // Hedef alma sýklýðý
    [SerializeField]
    protected float detectionRange = 50.0f; // Düþmanlarý algýlama menzili

    [Header("Enemy")]
    [SerializeField]
    protected Transform currentTarget; // Þu anki hedef

    void Start()
    {
        StartCoroutine(TargettingRoutine());
    }

    IEnumerator TargettingRoutine()
    {
        while (true)
        {
            TargetClosestEnemy();
            yield return new WaitForSeconds(targettingInterval);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    void TargetClosestEnemy()
    {
        Vector3 cachedPosition = transform.position;
        Collider[] hits = Physics.OverlapSphere(cachedPosition, detectionRange);
        
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }
        }

        currentTarget = closestEnemy;
        GameManager.instance.closestEnemy = closestEnemy;
    }
}
