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


    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
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

        GameObject[] oldMoveIcon = GameObject.FindGameObjectsWithTag("MoveIcon");

        foreach (GameObject moveIcon in oldMoveIcon)
        {
            if (this.transform.position.x == moveIcon.transform.position.x)
            {
                GameObject.Destroy(moveIcon);
            }
        }
    }

    void MoveIcon()
    {
        GameObject[] oldMoveIcon = GameObject.FindGameObjectsWithTag("MoveIcon");

        foreach (GameObject moveIcon in oldMoveIcon)
        {
            GameObject.Destroy(moveIcon);
        }

        Instantiate(moveIcon, (hitInfo.point + new Vector3(0.0f, 0.5f, 0.0f)), Quaternion.identity);
        
    }

}
