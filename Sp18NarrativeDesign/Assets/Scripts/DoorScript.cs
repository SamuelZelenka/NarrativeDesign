using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool locked = false;
    [SerializeField] bool open = true;
    [SerializeField] Animator animator;
    private void Start()
    {
        Open(open);
    }

    public void ToggleDoorOpen()
    {
        if (!locked)
        {
            open = !open;
            animator.SetBool("Open", open);
        }
    }

    public void Open(bool open)
    {
        switch (open)
        {
            case (true):
                if (!locked)
                {
                    animator.SetBool("Open", true);
                    open = true;
                }

                break;
            case (false):
                if (!locked)
                {
                    animator.SetBool("Open", false);
                    open = false;
                }
                break;
        }

    }


}
