using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public event Action<Objective> onObjectiveInteractAdd;
    public event Action<string> onObjectiveInteractComplete;
    public event Action objectiveComplete;


    public static List<Objective> activeObjectives = new List<Objective>();

    public static ObjectiveManager objectiveManager;
    public Text objectiveDescription;

    private void Awake()
    {
        objectiveManager = this;
    }


    public void ObjectiveInteractAdd(Objective newObjective)
    {
        if (onObjectiveInteractAdd != null)
        {
            onObjectiveInteractAdd(newObjective);
        }
    }

    public void ObjectiveInteractComplete(string completeTitle)
    {
        if (onObjectiveInteractAdd != null)
        {
            onObjectiveInteractComplete(completeTitle);
        }
    }
    public void ObjectiveComplete()
    {
        if (onObjectiveInteractAdd != null)
        {
            objectiveComplete();
        }
    }


    public static void UpdateObjectiveLog()
    {
        objectiveManager.objectiveDescription.text = "";
        foreach (Objective objective in activeObjectives)
        {
            if (objective.isCompleted)
            {
                objectiveManager.objectiveDescription.text += "(Completed) ";
            }
            objectiveManager.objectiveDescription.text += objective.objectiveText + "\n";
        }
    }
}
