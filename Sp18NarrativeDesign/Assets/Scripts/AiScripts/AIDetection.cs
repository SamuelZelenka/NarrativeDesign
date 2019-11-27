using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIDetection : MonoBehaviour
{

    public enum AIState { patrolling, pursuing, checking, stunned }
    [SerializeField] public AIState currentState;

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

    [SerializeField] Color lightPatrolColor;
    [SerializeField] Color lightPursuitColor;
    [SerializeField] Color lightCheckColor;
    [ColorUsage(true,true)][SerializeField] Color materialPatrolColor;
    [ColorUsage(true,true)][SerializeField] Color materialPursuitColor;
    [ColorUsage(true, true)] [SerializeField] Color materialCheckColor;


    [SerializeField] AudioSource movementAudioSource;
    [SerializeField] Renderer lens;
    [SerializeField] Light[] patrolLights;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        setLights(currentState);
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
        RaycastHit hit;
        Debug.DrawRay(gameObject.transform.position, transform.forward, Color.red);
        Debug.DrawLine(gameObject.transform.position, transform.position + transform.forward * viewDistance, Color.green);

        //      Debug.Log(rayCone(player, transform.position, transform.forward, angle));
        if (rayCone(player, transform.position, transform.forward, angle) && currentState != AIState.stunned)
        {


            if (detectedPlayer == true)
            {
                //gameOver(); //End the game if found
            }


            RaycastHit raycastHit;
            if (Physics.Linecast(transform.position, player.position, out raycastHit))
            {
                if (raycastHit.transform.tag == "Player")
                {
                    PlayerDetected();
                }
            }



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
                   if (waypoints.Count > 0) agent.SetDestination(waypoints[currentWaypointTarget].transform.position);
                    setLights(AIState.patrolling);
                }
                detectedPlayer = false;
                break;
            case AIState.stunned:

                if (stunTimer >= stunTime)
                {
                    currentState = prevState;
                    agent.SetDestination(prevDestination);
                    movementAudioSource.enabled = true;
                }
                else stunTimer += Time.deltaTime * 1;


                break;

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
        movementAudioSource.enabled = false;
    }
    public void PlayerDetected()
    {

        currentState = AIState.pursuing;
        detectedTimer = 0;
        detectedPlayer = true;
        setLights(AIState.pursuing);
    }
    public void CheckPosition(Vector3 positionToCheck)
    {
        
        if (currentState != AIState.pursuing && currentState != AIState.stunned)
        {

            currentState = AIState.checking;
            checkPositionTimer = 0;
            agent.SetDestination(positionToCheck);

            setLights(AIState.checking);
        }
    }

    void setLights(AIState aIState)
    {
//        Debug.Log(aIState);
        Color lensColor = new Color();
        Color lightColor = new Color();

        switch (aIState)
        {
            case AIState.patrolling:
                lensColor = materialPatrolColor;
                lightColor = lightPatrolColor;
                break;
            case AIState.pursuing:
                lensColor = materialPursuitColor;
                lightColor = lightPursuitColor;
                break;
            case AIState.checking:
                lensColor = materialCheckColor;
                lightColor = lightCheckColor;
                break;
            case AIState.stunned:
                //lensColor = materialPatrolColor;
                //lightColor = lightPatrolColor;
                break;
            default:
                break;
        }

        foreach (Light light in patrolLights)
        {
            light.color = lightColor;
        }
        lens.material.SetColor("_EmissionColor", lensColor);
    }


    bool rayCone(Transform player, Vector3 coneTipPos, Vector3 coneDirection, float angle)
    {
        float coneHalfAngle = angle / 2;
        Vector3 directionTowardT = player.position - coneTipPos;
        float angleFromConeCenter = Vector3.Angle(directionTowardT, coneDirection);
        return angleFromConeCenter <= coneHalfAngle;

    }

    //Function to end the game.
    void gameOver()
    {

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