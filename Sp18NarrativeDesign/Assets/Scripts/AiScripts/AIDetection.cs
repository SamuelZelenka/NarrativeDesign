using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetection : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float viewDistance = 5f;
    [SerializeField] float angle = 100;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(gameObject.transform.position, transform.forward, Color.red);
        Debug.DrawLine(gameObject.transform.position, transform.position + transform.forward * viewDistance, Color.green);

        Debug.Log(rayCone(player, transform.position, transform.forward, angle));
        if (rayCone(player, transform.position, transform.forward, angle))
        {
            //What happens when the player is discovered.
        }
    }
    bool rayCone(Transform player, Vector3 coneTipPos, Vector3 coneDirection, float angle)
    {
        float coneHalfAngle = angle / 2;
        Vector3 directionTowardT = player.position - coneTipPos;
        float angleFromConeCenter = Vector3.Angle(directionTowardT, coneDirection);
        return angleFromConeCenter <= coneHalfAngle;
    }
}