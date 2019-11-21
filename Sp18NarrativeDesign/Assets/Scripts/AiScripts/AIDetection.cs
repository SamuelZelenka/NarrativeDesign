using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIDetection : MonoBehaviour
{

   public enum AIState { patrolling, pursuing, checking, stunned }
    [SerializeField]public AIState currentState;
    
    [SerializeField] Transform player;
    [SerializeField] float viewDistance = 5f;
    [SerializeField] float angle = 100;

    [SerializeField] GameObject target; //current target for AI
    [SerializeField] float detectedTime = 10f;
    float detectedTimer = 0f;
    NavMeshAgent agent;
    public bool detectedPlayer = false;


    [SerializeField] float checkPositionTime = 10f;
    float checkPositionTimer = 0f;
    [SerializeField] List<PatrolWaypoint> waypoints = new List<PatrolWaypoint>();

    int currentWaypointTarget = 0;
    [SerializeField] float deadArea = 1f;

    [SerializeField] float waitAtWaypointTime = 1f;
    float waitAtWaypointTimer = 0f;
    float stunTime;
    float stunTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = gameObject.GetComponent<NavMeshAgent>();
        if (waypoints.Count > 0)
        {
            agent.SetDestination(waypoints[0].transform.position);
            waitAtWaypointTime = waypoints[currentWaypointTarget].waitHereTime;
            waitAtWaypointTimer = 0;
        }
    }

    private void OnEnable()
    {

        agent = gameObject.GetComponent<NavMeshAgent>();
        if (waypoints.Count > 0)
        {

            agent.SetDestination(waypoints[0].transform.position);
            waitAtWaypointTime = waypoints[currentWaypointTarget].waitHereTime;
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

        //      Debug.Log(rayCone(player, transform.position, transform.forward, angle));
        if (rayCone(player, transform.position, transform.forward, angle) && currentState != AIState.stunned)
        {
            RaycastHit raycastHit;
            if (Physics.Linecast(transform.position, player.position, out raycastHit))
            {
                if (raycastHit.transform.tag == "Player")
                {
                    Debug.Log("saodja");
                    PlayerDetected();
                }
            }



            //What happens when the player is discovered.
        }


        switch (currentState)
        {
            case AIState.patrolling:
                detectedPlayer = false;
                if (waypoints.Count > 0)
                {
                //    agent.SetDestination(waypoints[currentWaypointTarget].transform.position);
                    if (Vector3.Distance(transform.position, waypoints[currentWaypointTarget].transform.position) < deadArea)
                    {
                        if (waitAtWaypointTimer >= waitAtWaypointTime)
                        {
                            if (currentWaypointTarget + 1 >= waypoints.Count)
                            {
                                currentWaypointTarget = 0;

                            }
                            else
                            {
                                currentWaypointTarget++;


                            }
                            waitAtWaypointTimer = 0;
                            waitAtWaypointTime = waypoints[currentWaypointTarget].waitHereTime;

                            agent.SetDestination(waypoints[currentWaypointTarget].transform.position);
                        } else
                        {
                            waitAtWaypointTimer += Time.deltaTime * 1;
                        }
                    }
                }


                break;
            case AIState.pursuing:
                detectedTimer += Time.deltaTime * 1;
                agent.SetDestination(player.transform.position);
                if (detectedTimer >= detectedTime)
                {
                    currentState = AIState.checking;
                    CheckPosition(player.transform.position);
                }
                break;
            case AIState.checking:
                checkPositionTimer += Time.deltaTime * 1;
                if (checkPositionTimer >= checkPositionTime)
                {
                    currentState = AIState.patrolling;
                    agent.SetDestination(waypoints[currentWaypointTarget].transform.position);
                }
                detectedPlayer = false;
                break;
            case AIState.stunned:

                if (stunTimer >= stunTime)
                {
                    currentState = prevState;
                    agent.SetDestination(prevDestination);
                }
                else stunTimer += Time.deltaTime * 1;


                break;

        }

        if (detectedPlayer)
        {

        }
        else
        {





        }


    }

    AIState prevState;
    Vector3 prevDestination;
    public void Stun(float time)
    {
        stunTimer = 0;
        stunTime = time;
        prevState = currentState;
        currentState = AIState.stunned;
        prevDestination = agent.destination;
        agent.SetDestination(transform.position);
    }
    public void PlayerDetected()
    {
        Debug.Log("Detected player");
        currentState = AIState.pursuing;
        detectedTimer = 0;
        detectedPlayer = true;
    }
    public void CheckPosition(Vector3 positionToCheck)
    {
        
        if (currentState != AIState.pursuing && currentState != AIState.stunned)
        {
            Debug.Log("Checking: " + positionToCheck);
            currentState = AIState.checking;
            checkPositionTimer = 0;
            agent.SetDestination(positionToCheck);
        }
    }

    bool rayCone(Transform player, Vector3 coneTipPos, Vector3 coneDirection, float angle)
    {
        float coneHalfAngle = angle / 2;
        Vector3 directionTowardT = player.position - coneTipPos;
        float angleFromConeCenter = Vector3.Angle(directionTowardT, coneDirection);
        return angleFromConeCenter <= coneHalfAngle;

    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (i + 1 < waypoints.Count)
            {
                Gizmos.DrawLine(waypoints[i].transform.position, waypoints[i + 1].transform.position);
            }
            else
            {
                Gizmos.DrawLine(waypoints[i].transform.position, waypoints[0].transform.position);
            }
        }
        Gizmos.color = Color.red;
        if (agent != null)
            Gizmos.DrawLine(transform.position, agent.destination);

    }


}