using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveInteractable : Interactible
{
    [SerializeField] string objectiveText;

    public override void Interact()
    {
        ObjectiveManager.AddObjective(new Objective(objectiveText));
        Destroy(this.gameObject);
    }

}
