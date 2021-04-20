using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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
    private bool interactable;
   

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        moveIconRenderer = moveIcon.GetComponent<MeshRenderer>();

        playerAnimator = GetComponent<Animator>();

        moveIconRenderer.enabled = false;

        moveIcon.transform.parent = null;
    }

    void Update()
    {

        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Draw"))
        {
            interactable = false;
            Time.timeScale = 1;
        }

        else
        {
            interactable = true;
        }

        float distance = Vector3.Distance(moveIcon.transform.position, transform.position);

        if (Input.touchCount > 0 && interactable)
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
        if (Input.GetMouseButton(0) && interactable)
        {
            myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out hitInfo, 100, LayerMask.GetMask("Ground")))
            {
                Vector3 direction = (hitInfo.point - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100);

                myAgent.SetDestination(hitInfo.point);

                playerAnimator.SetBool("isMoving", true);

                Time.timeScale = 1.0f;

                MoveIcon();
            }
        }
        // End

        // check if player has arrived at location
        if (distance <= 0.5f && interactable)
        {
            moveIconRenderer.enabled = false;
            playerAnimator.SetBool("isMoving", false);
            Time.timeScale = slowTime;
        }

        //check if player has detected enemy and attack
        if (Physics.SphereCast(transform.position + new Vector3(0, 1, 0.5f), 0.75f, transform.forward, out RaycastHit sphereHitInfo, 1f))
        {
            if (sphereHitInfo.transform.tag == "Enemy")
            {
                playerAnimator.SetTrigger("attack");
                sphereHitInfo.transform.gameObject.GetComponent<EnemyGunman>().Death();
            }
        }
    }

    void MoveIcon()
    {
        moveIconRenderer.enabled = true;

        moveIcon.transform.position = hitInfo.point + new Vector3(0.0f, 0.5f, 0.0f); 
    }


    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
