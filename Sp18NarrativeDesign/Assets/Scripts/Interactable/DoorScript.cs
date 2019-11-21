using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool locked = false;
    [SerializeField] bool open = true;
    [SerializeField] Animator animator;
    [Tooltip("Should the door close by itself")]
    [SerializeField] bool autoClose = false;
    [Tooltip("The time until the door closes by itself")]
    [SerializeField] float timeUntilClose = 1f;
    float doorTimer = 0;

    private void Start()
    {
        Open(open);
    }

    public void ToggleDoorOpen()
    {

        Open(!open);

    }
    private void Update()
    {
        //   Debug.Log(doorTimer);
        if (autoClose)
        {
            if (doorTimer > timeUntilClose)
            {
                Open(false);
            }
            else
                doorTimer += 1 * Time.deltaTime;

        }
    }

    public void Open(bool shouldOpen)
    {
        if (!locked)
        {
            animator.SetBool("Open", shouldOpen);
            doorTimer = 0;
            open = shouldOpen;
            FindObjectOfType<AudioManager>().Play("SlidingDoor");
        }

    }


}
