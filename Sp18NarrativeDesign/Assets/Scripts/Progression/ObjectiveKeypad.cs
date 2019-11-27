using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveKeypad : Interactible
{
    [SerializeField] DoorScript door;
    [SerializeField] Renderer lightRenderer;

    public override void Start()
    {
        base.Start();
        if (!door.locked)
        {
            lightRenderer.material.color = Color.green;
        }
        else
        {
            lightRenderer.material.color = Color.red;
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (!door.locked)
        {
            door.ToggleDoorOpen();
            lightRenderer.material.color = Color.green;
        }
        else
        {
            lightRenderer.material.color = Color.red;
        }

    }

}
