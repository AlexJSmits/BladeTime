using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;

    private void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            GetComponent<SphereCollider>().enabled = false;
        }

        else
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetComponent<SphereCollider>().enabled = true;
        }
    }

}
