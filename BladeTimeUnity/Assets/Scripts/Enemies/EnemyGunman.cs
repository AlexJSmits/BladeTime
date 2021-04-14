using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGunman : MonoBehaviour
{
    public float lookRadius = 10;

    Transform target;

    NavMeshAgent agent;

    public float shootDelay;
    public float aimTime = 0.5f;
    public GameObject bullet;
    public Transform gunEnd;


    private float reloadTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        reloadTime = shootDelay;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                Invoke ("Shoot", aimTime);
            }
        }
        
        if (shootDelay > 0)
        {
            shootDelay -= 1 * Time.deltaTime;
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    void Shoot()
    {
        if (shootDelay <= 0)
        {
            GameObject bulletObject = Instantiate(bullet, gunEnd.transform.position, Quaternion.identity);
            bulletObject.transform.forward = gunEnd.forward;
            shootDelay = reloadTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
