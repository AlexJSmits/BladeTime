using System.Security.Cryptography;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{


    public LayerMask whatCanBePressedOn;
    public GameObject moveIcon;
    public float slowTime = 0.2f;

    private RaycastHit hitInfo;
    private Ray myRay;
    private NavMeshAgent myAgent;
    private Touch touch1;
    private MeshRenderer moveIconRenderer;

   

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        moveIconRenderer = moveIcon.GetComponent<MeshRenderer>();

        moveIconRenderer.enabled = false;

        moveIcon.transform.parent = null;

        Time.timeScale = slowTime;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch1= Input.GetTouch(0);

            myRay = Camera.main.ScreenPointToRay(touch1.position);

            if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBePressedOn))
            {
                Vector3 direction = (hitInfo.point - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1);

                myAgent.SetDestination(hitInfo.point);

                Time.timeScale = 1.0f;

                MoveIcon();
            }
        }

        if (this.transform.position.x == moveIcon.transform.position.x)
        {
            moveIconRenderer.enabled = false;

            Time.timeScale = slowTime;
        }

    }

    void MoveIcon()
    {
        moveIconRenderer.enabled = true;

        moveIcon.transform.position = hitInfo.point + new Vector3(0.0f, 0.5f, 0.0f); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }

}
