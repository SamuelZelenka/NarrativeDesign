using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ObjectiveInteractable : Interactible
{
    public enum ObjectiveType { Start, End, Both}

    public ObjectiveType myType;
    [HideInInspector]
    public string startTitle;
    [HideInInspector]
    public string endTitle;
    [HideInInspector]
    public string objectiveText;

    Objective objective;

    public override void Interact()
    {
        objective = new Objective(startTitle, objectiveText);
        switch (myType)
        {
            case ObjectiveType.Start:
                ObjectiveManager.objectiveManager.ObjectiveInteractAdd(objective);
                break;
            case ObjectiveType.End:
                ObjectiveManager.objectiveManager.ObjectiveInteractComplete(endTitle);
                break;    case ObjectiveType.Both:
                ObjectiveManager.objectiveManager.ObjectiveInteractComplete(endTitle);
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
public class TestCustomInspector : Editor
{

    public override void OnInspectorGUI()
    {
        ObjectiveInteractable script = (ObjectiveInteractable)target;

        script.myType = (ObjectiveInteractable.ObjectiveType)EditorGUILayout.EnumPopup("My type", script.myType);


        switch (script.myType)
        {
            case ObjectiveInteractable.ObjectiveType.Start:
                script.startTitle = EditorGUILayout.TextField("Objective Title", script.startTitle);
                script.objectiveText = EditorGUILayout.TextField("Objective Text", script.objectiveText);
                break;
            case ObjectiveInteractable.ObjectiveType.End:
                script.endTitle = EditorGUILayout.TextField("Complete Title", script.endTitle);
                break;
            case ObjectiveInteractable.ObjectiveType.Both:
                script.startTitle = EditorGUILayout.TextField("Objective Title", script.startTitle);
                script.objectiveText = EditorGUILayout.TextField("Objective Text", script.objectiveText);
                script.endTitle = EditorGUILayout.TextField("Complete Title", script.endTitle);
                break;
            default:
                break;
        }
    }
}
#endif