using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    [SerializeField] float s_WalkingSound;
    [SerializeField] float s_RunningSound;

    float _CurrentSpeed;
    void Start()
    {
        Debug.Log("Hello World");
    }

    void Update()
    {
        _CurrentSpeed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        Debug.Log("Current Speed is: " + _CurrentSpeed);

        if (Input.GetKey(KeyCode.W))
        {
            Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, s_WalkingSound);
            foreach (var item in hitColliders)
            {
                if (item.tag == "Enemy")
                {
                    item.GetComponent<AIDetection>().CheckPosition(gameObject.transform.position);
                }
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, s_RunningSound);
            foreach (var item in hitColliders)
            {
                if (item.tag == "Enemy")
                {
                    item.GetComponent<AIDetection>().CheckPosition(gameObject.transform.position);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, s_WalkingSound);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(gameObject.transform.position, s_RunningSound);
        }
    }
}
