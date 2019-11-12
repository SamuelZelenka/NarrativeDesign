using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionTracker : MonoBehaviour
{
    List<Objective> completedObjectives;

    Objective currentObjective;

    void SetObjective(Objective objective)
    {
        completedObjectives.Add(currentObjective);

        currentObjective = objective;
    }
}
