using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveKeypad : Interactible
{
    [SerializeField] DoorScript door;
    [SerializeField] Renderer lightRenderer;
    [SerializeField] Light lt; // Mohamed
    public override void Start()
    {
       

        base.Start();
        if (!door.locked)
        {
            lightRenderer.material.color = Color.green;
            lt = GetComponent<Light>(); // Mohamed
        }
        else
        {
            lightRenderer.material.color = Color.red;
            lt = GetComponent<Light>(); // mohamed 
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (!door.locked)
        {
            door.ToggleDoorOpen();
            lightRenderer.material.color = Color.green;
            lt = GetComponent<Light>();
        }
        else
        {
            lightRenderer.material.color = Color.red;
            lt = GetComponent<Light>(); // Mohamed
        }

    }

    //public void ToggleLight()
    //{

    //    if (!door.locked)
    //    {
    //        lightRenderer.material.color = Color.green;
    //    }
    //    else
    //    {
    //        lightRenderer.material.color = Color.red;
    //    }
    //}
}

  
