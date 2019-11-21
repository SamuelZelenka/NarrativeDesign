using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Objective
{
    public int ID;
    public string objectiveText;
    public bool isCompleted;

   


    public Objective(int ID, string objectiveText)
    {
        this.ID = ID;
        this.objectiveText = objectiveText;
        this.isCompleted = false;
        ObjectiveManager.objectiveManager.onObjectiveInteractAdd += OnInteractAdd;
        ObjectiveManager.objectiveManager.onObjectiveInteractComplete += OnInteractComplete;
    }


    public void OnInteractAdd(Objective newObjective)
    {

        if (!ObjectiveManager.activeObjectives.Contains(this))
        {
            ObjectiveManager.activeObjectives.Add(this);
        }
    }
    public void OnInteractComplete(int completeID)
    {
        foreach (Objective objective in ObjectiveManager.activeObjectives)
        {
            if (objective.ID == completeID && !objective.isCompleted)
            {
                objective.isCompleted = true;
                ObjectiveManager.objectiveManager.ObjectiveComplete();
            }
        }
    }


}
