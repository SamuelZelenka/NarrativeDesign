using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<ObjectiveInteractable> completedObjectives = new List<ObjectiveInteractable>();

    public List<ObjectiveInteractable> activeObjectives = new List<ObjectiveInteractable>();

    public static ObjectiveManager objectiveManager;
    private void Start()
    {
        objectiveManager = Transform.FindObjectOfType<ObjectiveManager>();
    }


    public static string AddObjective(ObjectiveInteractable objective)
    {

        if (!objectiveManager.activeObjectives.Contains(objective))
        {
            objectiveManager.activeObjectives.Add(objective);
            
            return $"{objective.objectiveTitle} added to objectives.";
        }
        return "Objective is already active.";
    }
    public static string CompleteObjective(ObjectiveInteractable objective)
    {
        Predicate<ObjectiveInteractable> objectFinder = (ObjectiveInteractable activeObject) => { return activeObject == objective;};
        if (!objectiveManager.completedObjectives.Contains(objective))
        {
            objectiveManager.completedObjectives.Add(objective);
            objectiveManager.activeObjectives.RemoveAt(objectiveManager.activeObjectives.FindIndex(objectFinder));
            return $"{objective.objectiveTitle} Objective Completed.";
        }
        return "Objective is already active.";
    }
    
}
