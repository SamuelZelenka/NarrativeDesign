using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : Interactible
{
    public string objectiveTitle;
    public override void Interact()
    {
        string newstring = ProgressionTracker.AddObjective(this);
        print(newstring);
        Destroy(this.gameObject);
    }

}
