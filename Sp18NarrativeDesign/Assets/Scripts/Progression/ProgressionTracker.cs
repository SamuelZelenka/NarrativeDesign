using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProgressionTracker
{
    public static List<Objective> completedObjectives = new List<Objective>();

    public static List<Objective> activeObjectives = new List<Objective>();



    public static string AddObjective(Objective objective)
    {
        if (!activeObjectives.Contains(objective))
        {
            activeObjectives.Add(objective);
            return $"{objective.objectiveTitle} added to objectives.";
        }
        return "Objective is already active.";
    }
    public static string CompleteObjective(Objective objective)
    {
        Predicate<Objective> objectFinder = (Objective activeObject) => { return activeObject == objective;};
        if (!completedObjectives.Contains(objective))
        {
            completedObjectives.Add(objective);
            activeObjectives.RemoveAt(activeObjectives.FindIndex(objectFinder));
            return $"{objective.objectiveTitle} Objective Completed.";
        }
        return "Objective is already active.";
    }
    
}
