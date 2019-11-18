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


    AIDetection AIDetection
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
        if (!AIDetection.detectedPlayer && !stunned)
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
                stunnedEffect.SetActive(false);
                if (animator != null)
                {
                    animator.SetBool("Stunned", false);
                }
                //         GetComponent<NavMeshAgent>().enabled = true;
            }

        }

    }

    public void StartStun()
    {
        stunTimer = 0;
        // AIDetection.enabled = false;'
        AIDetection.Stun(stunnedTime);
        stunned = true;
        stunnedEffect.SetActive(true);
        if (animator != null)
        {
            animator.SetBool("Stunned", true);
        }
        audioSource.Play();
        // GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }


}
