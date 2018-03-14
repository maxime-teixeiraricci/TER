
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ActionUnit))]
[CanEditMultipleObjects]

public class ActionsEditorShow : Editor
{
    ActionUnit myTarget;

    void OnEnable()
    {
        myTarget = (ActionUnit)target;
    }

    public override void OnInspectorGUI()
    {
        ActionUnit myTarget = (ActionUnit)target;
        GUILayout.Label("Possible actions :", EditorStyles.boldLabel);
        foreach (string key in myTarget._actions.Keys)
        {
            EditorGUILayout.LabelField("> " + key);
        }
    }
}