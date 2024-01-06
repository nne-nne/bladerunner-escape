using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cable))]
public class CableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Cable cable = (Cable)target;

        if (GUILayout.Button("Generate"))
        {
            cable.Generate();
        }
    }
}

