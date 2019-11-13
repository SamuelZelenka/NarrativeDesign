using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaypoints : MonoBehaviour
{
    [SerializeField] GameObject waypoints1;
    [SerializeField] GameObject waypoints2;
    [SerializeField] GameObject waypoints3;
    [SerializeField] GameObject waypoints4;
    [SerializeField] float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.position != waypoints1.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints2.transform.position, speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Var han är, vart han ska.
        if (gameObject.transform.position == waypoints1.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints2.transform.position, speed * Time.deltaTime);
        }
        if (gameObject.transform == waypoints2)
        {
            gameObject.transform.position = waypoints3.transform.position;
        }
        if (gameObject.transform == waypoints3)
        {
            gameObject.transform.position = waypoints4.transform.position;
        }
        if (gameObject.transform == waypoints4)
        {
            gameObject.transform.position = waypoints1.transform.position;
        }

    }
}
