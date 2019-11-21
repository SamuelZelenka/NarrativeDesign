using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectiveTrigger : MonoBehaviour
{
    public enum ObjectiveType { Start, End, Both }

    [HideInInspector]
    public ObjectiveType myType;
    [HideInInspector]
    public int startID;
    [HideInInspector]
    public int endID;
    [HideInInspector]
    public string objectiveText;

    Objective objective;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
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
}
#if UNITY_EDITOR
[CustomEditor(typeof(ObjectiveTrigger))]
public class ObjectiveTriggerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ObjectiveTrigger script = (ObjectiveTrigger)target;

        script.myType = (ObjectiveTrigger.ObjectiveType)EditorGUILayout.EnumPopup("My type", script.myType);


        switch (script.myType)
        {
            case ObjectiveTrigger.ObjectiveType.Start:
                script.startID = EditorGUILayout.IntField("Objective Title ID", script.startID);
                script.objectiveText = EditorGUILayout.TextField("Objective Text", script.objectiveText);
                break;
            case ObjectiveTrigger.ObjectiveType.End:
                script.endID = EditorGUILayout.IntField("Complete Title with ID", script.endID);
                break;
            case ObjectiveTrigger.ObjectiveType.Both:
                script.startID = EditorGUILayout.IntField("Objective Title ID", script.startID);
                script.objectiveText = EditorGUILayout.TextField("Objective Text", script.objectiveText);
                script.endID = EditorGUILayout.IntField("Complete Title with ID", script.endID);
                break;
            default:
                break;
        }
    }
}
#endif