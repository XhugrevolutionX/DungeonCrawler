using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeneratorPCGNoCoroutine))]
public class GeneratorPCG_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        
        GeneratorPCGNoCoroutine generator = (GeneratorPCGNoCoroutine)target;
        //base.OnInspectorGUI();
        DrawDefaultInspector();
        
        EditorGUILayout.Space(20);
        if (GUILayout.Button("Generate"))
        {
            generator.RestartGeneration();
        }

        if (GUILayout.Button("Clean"))
        {
            generator.StopGeneration();
        }
        
    }
}
