using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGunman : MonoBehaviour
{
    public float lookRadius = 10;

    Transform target;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
