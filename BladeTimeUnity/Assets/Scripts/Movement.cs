using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public GameObject moveIcon;
    public float slowTime = 0.4f;

    private RaycastHit hitInfo;
    private Ray myRay;
    private NavMeshAgent myAgent;
    private Touch touch1;
    private MeshRenderer moveIconRenderer;
    private Animator playerAnimator;
   

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        moveIconRenderer = moveIcon.GetComponent<MeshRenderer>();

        playerAnimator = GetComponent<Animator>();

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

            if (Physics.Raycast(myRay, out hitInfo, 100, LayerMask.GetMask("Ground")))
            {
                Vector3 direction = (hitInfo.point - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);

                myAgent.SetDestination(hitInfo.point);

                playerAnimator.SetBool("isMoving", true);

                Time.timeScale = 1.0f;
                

                MoveIcon();
            }
        }

        // For PC Testing Purposes Only
        if (Input.GetMouseButton(0))
        {
            myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out hitInfo, 100, LayerMask.GetMask("Ground")))
            {
                Vector3 direction = (hitInfo.point - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1);

                myAgent.SetDestination(hitInfo.point);

                playerAnimator.SetBool("isMoving", true);

                Time.timeScale = 1.0f;

                MoveIcon();
            }
        }
        // End

        if (this.transform.position.x == moveIcon.transform.position.x)
        {
            moveIconRenderer.enabled = false;
            playerAnimator.SetBool("isMoving", false);
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
            Ray ray = new Ray(transform.position, other.gameObject.transform.position);
            if (Physics.Raycast(ray, out hitInfo) && other.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyGunman>().Death();
            }
        }
    }

}
