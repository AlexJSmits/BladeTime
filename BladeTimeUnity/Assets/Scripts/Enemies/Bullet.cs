using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private RaycastHit hitInfo;

    private void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.1f))
        {
            if (hitInfo.transform.tag == "Player")
            {
                hitInfo.transform.GetComponent<Movement>().Death();
                Destroy(gameObject);
            }

            if (hitInfo.transform.tag != "Player" || hitInfo.transform.tag == null)
            {
                Debug.Log("Not Player");
                Destroy(gameObject);
            }
        }

    }
}
