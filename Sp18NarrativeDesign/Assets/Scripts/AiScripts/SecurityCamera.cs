using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityCamera : MonoBehaviour
{
    Transform player;
    [SerializeField] float viewDistance = 5f;
    [SerializeField] float angle = 30;
    Light _Light;
    [SerializeField] float _LightTimer;
   
    GameObject[] _Enemies;
    Color _Color;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _Light = gameObject.GetComponent<Light>();
    }

    private void Update()
    {
        if (rayCone(player, transform.position, transform.forward, angle))
        {
            RaycastHit raycastHit;

            Debug.DrawRay(gameObject.transform.position, gameObject.transform.position, Color.red);

            if (Physics.Linecast(transform.position, player.position, out raycastHit))
            {
                if (raycastHit.transform.tag == "Player")
                {
                    _Light.color = new Color(0.8509804f, 0.05098039f, 0.2627451f, 1);
                    foreach (var item in _Enemies)
                    {
                        item.GetComponent<AIDetection>().PlayerDetected();
                    }
                    StartCoroutine(Timer());
                }
            }
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(_LightTimer); //should match the amount of time the AI has pursue
        _Light.color = new Color(1,1,1,1);

    }
    bool rayCone(Transform player, Vector3 coneTipPos, Vector3 coneDirection, float angle)
    {
        float coneHalfAngle = angle / 2;
        Vector3 directionTowardT = player.position - coneTipPos;
        float angleFromConeCenter = Vector3.Angle(directionTowardT, coneDirection);
        return angleFromConeCenter <= coneHalfAngle;
    }
}
