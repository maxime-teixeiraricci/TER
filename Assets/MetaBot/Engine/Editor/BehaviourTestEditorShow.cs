
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TestUnitBehaviour))]
[CanEditMultipleObjects]

public class BehaviourTestEditorShow : Editor
{
    TestUnitBehaviour myTarget;

    void OnEnable()
    {
        myTarget = (TestUnitBehaviour)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Create Default Team"))
        {
            myTarget.CreateDefaultBehaviour();
        }
    }
}