using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Objective
{
    public string title;
    public string objectiveText;
    public bool isCompleted;

   


    public Objective(string title, string objectiveText)
    {
        this.title = title;
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
    public void OnInteractComplete(string completeTitle)
    {
        foreach (Objective objective in ObjectiveManager.activeObjectives)
        {
            if (objective.title == completeTitle && !objective.isCompleted)
            {
                objective.isCompleted = true;
                ObjectiveManager.objectiveManager.ObjectiveComplete();
            }
        }
    }


}
