using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public event Action<Objective> onObjectiveInteractAdd;
    public event Action<int> onObjectiveInteractComplete;
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

    public void ObjectiveInteractComplete(int completeID)
    {
        if (onObjectiveInteractAdd != null)
        {
            onObjectiveInteractComplete(completeID);
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
