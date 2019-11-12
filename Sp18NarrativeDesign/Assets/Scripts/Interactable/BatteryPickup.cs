using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : Interactible
{
    [SerializeField] float percentageRestore = 20;
    public void PickUpBattery(float percentage)
    {
        foreach (var item in GameObject.FindObjectsOfType<PlayerXray>())
        {
            item.PickUpBattery(percentage);
        }
    }

    public override bool CanInteract
    {
        get
        {
            if (interactible)
            {
                if (FindObjectOfType<PlayerXray>().BatteryPercentage == 100) return false;
                Collider[] objs = Physics.OverlapSphere(InteractionPoint, interactionRadius, interactionMask);
                foreach (Collider item in objs)
                {
                    if (item.gameObject.CompareTag("Player"))
                    {
                        return true;
                    }
                }
                return false;

            }
            else return false;
        }
    }

    public override void Interact()
    {
        
        PickUpBattery(percentageRestore);
        base.Interact();
        Destroy(gameObject);
    }
}

