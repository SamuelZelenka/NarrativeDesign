using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveInteractable : Interactible
{
    public string objectiveTitle;
    public override void Interact()
    {
        ObjectiveManager.AddObjective(this);
        Destroy(this.gameObject);
    }
    ontrigger
}
