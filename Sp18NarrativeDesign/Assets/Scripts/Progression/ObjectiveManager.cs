using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> completedObjectives = new List<Objective>();

    public List<Objective> activeObjectives = new List<Objective>();

    [SerializeField] Text objectiveText;
    [SerializeField] int count;

    public static ObjectiveManager objectiveManager;
    public static Text objectiveDescription;
    private void Start()
    {
        objectiveManager = this.GetComponent<ObjectiveManager>();
        objectiveDescription = objectiveText; 
    }
    private void Update()
    {
        count = activeObjectives.Count;
    }


    public static string AddObjective(Objective objective)
    {

        if (!objectiveManager.activeObjectives.Contains(objective))
        {
            objectiveManager.activeObjectives.Add(objective);
            objectiveDescription.text += objective.objectiveText + "\n";
            return $"{objective.objectiveText} added to objectives.";
        }
        return "Objective is already active.";
    }
    public static string CompleteObjective(Objective objective)
    {
        Predicate<Objective> objectFinder = (Objective activeObject) => { return activeObject == objective;};
        if (!objectiveManager.completedObjectives.Contains(objective))
        {
            objectiveManager.completedObjectives.Add(objective);
            objectiveManager.activeObjectives.RemoveAt(objectiveManager.activeObjectives.FindIndex(objectFinder));
            return $"{objective.objectiveText} Objective Completed.";
        }
        return "Objective is already active.";
    }
    
}
