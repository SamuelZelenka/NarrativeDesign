using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIDetection : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float viewDistance = 5f;
    [SerializeField] float angle = 100;

    [SerializeField] GameObject target; //current target for AI

    NavMeshAgent agent;
    string waypointTag = "Waypoint";
    public bool detectedPlayer = false;
    List<GameObject> waypointList = new List<GameObject>();// list of possible waypoints

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = gameObject.GetComponent<NavMeshAgent>();
        foreach (var item in GameObject.FindGameObjectsWithTag(waypointTag))
        {
            waypointList.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (target != null)
        //{
        //    target = GameObject.FindGameObjectWithTag("Player");
        ////}

        //agent.SetDestination(target.transform.position); //Sets the target for the AI

        RaycastHit hit;
        Debug.DrawRay(gameObject.transform.position, transform.forward, Color.red);
        Debug.DrawLine(gameObject.transform.position, transform.position + transform.forward * viewDistance, Color.green);

        Debug.Log(rayCone(player, transform.position, transform.forward, angle));
        if (rayCone(player, transform.position, transform.forward, angle))
        {
            agent.SetDestination(player.transform.position);
            detectedPlayer = true;
            if (detectedPlayer == true)
            {
                //gameOver(); //End the game if found
            }
            //What happens when the player is discovered.
        } else { detectedPlayer = false; }
    }
    bool rayCone(Transform player, Vector3 coneTipPos, Vector3 coneDirection, float angle)
    {
        float coneHalfAngle = angle / 2;
        Vector3 directionTowardT = player.position - coneTipPos;
        float angleFromConeCenter = Vector3.Angle(directionTowardT, coneDirection);
        return angleFromConeCenter <= coneHalfAngle;
    }
    //Function to end the game.
    //void gameOver()
    //{
        
    //}
}