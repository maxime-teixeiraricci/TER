//c# Example (LookAtPointEditor.cs)
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Percept), true )]
[CanEditMultipleObjects]

public class LookAtPointEditor : Editor
{
    Percept myTarget;

    void OnEnable()
    {
        myTarget = (Percept)target;
    }

    public override void OnInspectorGUI()
    {
        Percept myTarget = (Percept)target;
        GUILayout.Label("Percepts :", EditorStyles.boldLabel);
        
        
        foreach (string key in myTarget._percepts.Keys)
        {

            GUIStyle s = new GUIStyle();
            s.normal.textColor = new Color(0.6f, 0.1f, 0.1f);
            if (myTarget._percepts[key]()) { s.normal.textColor = new Color(0.1f,0.6f, 0.1f); }
            EditorGUILayout.LabelField("> " + key, s);
        }
    }
}