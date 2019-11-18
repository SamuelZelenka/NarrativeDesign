using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class SoundWave : MonoBehaviour
{
    [SerializeField] float s_WalkingSound;
    [SerializeField] float s_RunningSound;
    
    bool Crouching
    {
        get
        {
            return GetComponent<ThirdPersonCharacter>().m_Crouching;
        }
    }


    float _CurrentSpeed;
    void Start()
    {
        Debug.Log("Hello World");
    }

    void Update()
    {
        _CurrentSpeed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        //Debug.Log("Current Speed is: " + _CurrentSpeed);
        if (!Crouching)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > .2f || Mathf.Abs(Input.GetAxis("Vertical")) > .2f)
            {
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
                else
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
