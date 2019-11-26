using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveKeypad : Interactible
{
    [SerializeField] DoorScript door;
    [SerializeField] Material lightMaterial;

    public override void Start()
    {
        lightMaterial.color = Color.red;
    }

    public override void Interact()
    {
        base.Interact();
        if (!door.locked)
        {
            door.ToggleDoorOpen();
            lightMaterial.color = Color.green;
        }
        else
        {
            lightMaterial.color = Color.red;
        }

    }

}
