//c# Example (LookAtPointEditor.cs)
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Percept), true )]
[CanEditMultipleObjects]

public class LookAtPointEditor : Editor
{
    Percept myTarget;
    bool showPercept = false;

    void OnEnable()
    {
        myTarget = (Percept)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Percept myTarget = (Percept)target;
        GUILayout.Label("Percepts :", EditorStyles.boldLabel);

        if(GUILayout.Button((showPercept) ? "Hide Percepts" : "Show Percepts"))
        {
            showPercept = !showPercept;
        }
        if (showPercept)
        {
            foreach (string key in myTarget._percepts.Keys)
            {

                GUIStyle s = new GUIStyle();
                s.normal.textColor = new Color(0.6f, 0.1f, 0.1f);
                if (myTarget._percepts[key]()) { s.normal.textColor = new Color(0.1f, 0.6f, 0.1f); }
                EditorGUILayout.LabelField("> " + key, s);
            }
        }
        
    }
}