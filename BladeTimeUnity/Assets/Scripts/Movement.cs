using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

    public LayerMask whatCanBePressedOn;

    private NavMeshAgent myAgent;

    public GameObject moveIcon;

    private RaycastHit hitInfo;
    private Ray myRay;

    private Touch touch1;

    private MeshRenderer moveIconRenderer;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        moveIconRenderer = moveIcon.GetComponent<MeshRenderer>();

        moveIconRenderer.enabled = false;

        moveIcon.transform.parent = null;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch1= Input.GetTouch(0);

            myRay = Camera.main.ScreenPointToRay(touch1.position);

            if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBePressedOn))
            {
                myAgent.SetDestination(hitInfo.point);

                MoveIcon();
            }
        }

        if (this.transform.position.x == moveIcon.transform.position.x)
        {
            moveIconRenderer.enabled = false;
        }

    }

    void MoveIcon()
    {

        moveIconRenderer.enabled = true;

        moveIcon.transform.position = hitInfo.point + new Vector3(0.0f, 0.5f, 0.0f);
        
    }

}
