using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Brain))]
[CanEditMultipleObjects]
public class BrainEditorShow : Editor
{
    Brain myTarget;

    void OnEnable()
    {
        myTarget = (Brain)target;
    }

    public override void OnInspectorGUI()
    {
        Brain myTarget = (Brain)target;
        GUILayout.Label("Current Action :", EditorStyles.boldLabel);
        if (myTarget._instructions != null)
        {
            EditorGUILayout.LabelField(">>> " + myTarget.NextAction());
        }
    }
}