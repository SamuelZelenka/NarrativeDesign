using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIStun : Interactible
{
    AIDetection aIDetection;

    float stunTimer = 0;
    [SerializeField] float stunnedTime = 3;
    bool stunned = false;
    [SerializeField] GameObject stunnedEffect;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator animator;
    [SerializeField] string stunnedParameter = "Stunned";
    [SerializeField] LineRenderer lineRenderer = new LineRenderer();

    AIDetection _AIDetection
    {
        get
        {
            if (aIDetection == null) aIDetection = GetComponent<AIDetection>();
            return aIDetection;
        }
    }

    public override void Start()
    {
        base.Start();
        stunnedEffect.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
        if ( _AIDetection.currentState != AIDetection.AIState.pursuing && !stunned)
        {
            interactible = true;
        }
        else
        {
            interactible = false;
        }

        if (stunned)
        {
            stunTimer += 1 * Time.deltaTime;
            if (stunTimer >= stunnedTime)
            {
                //   AIDetection.enabled = true;
                stunned = false;
             //   stunnedEffect.SetActive(false);
                if (animator != null)
                {
                    Debug.Log("Falsed;");
                    animator.SetBool("Stunned", false);
                }
                //         GetComponent<NavMeshAgent>().enabled = true;
            }

        }
        //lineRenderer.SetPosition(0, FindObjectOfType<PlayerXray>().transform.position);
        //lineRenderer.SetPosition(1, stunnedEffect.transform.position);
    }

    public void StartStun()
    {
        stunTimer = 0;
        // AIDetection.enabled = false;'
        _AIDetection.Stun(stunnedTime);
        stunned = true;
        //stunnedEffect.SetActive(true);
        if (animator != null)
        {
            Debug.Log("Truthed");
            animator.SetBool("Stunned", true);
        }
        audioSource.Play();
        // GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }


}
