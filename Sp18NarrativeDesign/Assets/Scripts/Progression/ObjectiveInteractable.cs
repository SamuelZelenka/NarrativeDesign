using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ObjectiveInteractable : Interactible
{
    public enum ObjectiveType { Start, End, Both}

    [HideInInspector] public ObjectiveType myType;
    [HideInInspector] public int startID;
    [HideInInspector] public int endID;
    [HideInInspector] public string objectiveText;

    Objective objective;

    public override void Interact()
    {
        base.Interact();
        objective = new Objective(startID, objectiveText);
        switch (myType)
        {
            case ObjectiveType.Start:
                ObjectiveManager.objectiveManager.ObjectiveInteractAdd(objective);
                break;
            case ObjectiveType.End:
                ObjectiveManager.objectiveManager.ObjectiveInteractComplete(endID);
                break;    
            case ObjectiveType.Both:
                ObjectiveManager.objectiveManager.ObjectiveInteractComplete(endID);
                ObjectiveManager.objectiveManager.ObjectiveInteractAdd(objective);
                break;
            default:
                break;
        }

        
        ObjectiveManager.UpdateObjectiveLog();
        Destroy(this.gameObject);
    }

}

#if UNITY_EDITOR

[CustomEditor(typeof(ObjectiveInteractable))]
public class ObjectiveInteractableEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ObjectiveInteractable script = (ObjectiveInteractable)target;

        script.myType = (ObjectiveInteractable.ObjectiveType)EditorGUILayout.EnumPopup("My type", script.myType);


        switch (script.myType)
        {
            case ObjectiveInteractable.ObjectiveType.Start:
                script.startID = EditorGUILayout.IntField("Objective Title", script.startID);
                script.objectiveText = EditorGUILayout.TextField("Objective Text", script.objectiveText);
                break;
            case ObjectiveInteractable.ObjectiveType.End:
                script.endID = EditorGUILayout.IntField("Complete Title", script.endID);
                break;
            case ObjectiveInteractable.ObjectiveType.Both:
                script.startID = EditorGUILayout.IntField("Objective Title", script.startID);
                script.objectiveText = EditorGUILayout.TextField("Objective Text", script.objectiveText);
                script.endID = EditorGUILayout.IntField("Complete Title", script.endID);
                break;
            default:
                break;
        }
    }
}
#endif