using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] GameObject target; //current target for AI

    NavMeshAgent agent;
    string waypointTag = "Waypoint";

    List<GameObject> waypointList = new List<GameObject>();// list of possible waypoints

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        foreach (var item in GameObject.FindGameObjectsWithTag(waypointTag))
        {
            waypointList.Add(item);
        }
    }

    void Update()
    {
        if (target != null) {target = GameObject.FindGameObjectWithTag("Player"); }

        agent.SetDestination(target.transform.position); //Sets the target for the AI
    }

    IEnumerator PatrolPath()
    {
        //first waypoint
        yield return new WaitForSeconds(5f);
        //next point     
    }
}
