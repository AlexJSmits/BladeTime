using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class EnemyGunman : MonoBehaviour
{
    public float lookRadius = 10;

    Transform target;

    NavMeshAgent agent;

    public float shootDelay;
    public float aimDelay;
    public GameObject bullet;
    public Transform[] gunEnd;

    private ParticleSystem blood;
    private float reloadTime;
    private bool isReady = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        reloadTime = shootDelay;
        blood = GetComponentInChildren<ParticleSystem>();
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
                Shoot();
            }
        }
        
        if (shootDelay > 0)
        {
            shootDelay -= 1 * Time.deltaTime;
        }

    }

    void FaceTarget()
    {
        if (isReady)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }
        
    }

    void Shoot()
    {
        if (shootDelay <= 0)
        {
            isReady = false;
            Invoke("ReadyUp", aimDelay);

            foreach (Transform gunEnd in gunEnd)
            {
                if (gunEnd != null)
                {
                    GameObject bulletObject = Instantiate(bullet, gunEnd.transform.position, Quaternion.identity);
                    bulletObject.transform.forward = gunEnd.forward;
                    shootDelay = reloadTime;
                }
            }
        }
    }

    void ReadyUp()
    {
        isReady = true;
    }

    public void Death()
    {
        Destroy(GetComponent<EnemyGunman>());
        GetComponent<CapsuleCollider>().enabled = false;
        Destroy(GetComponent<Rigidbody>());
        blood.Play();
        blood.transform.parent = null;
        GetComponent<Animator>().enabled = true;
        Destroy(gameObject, 1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
