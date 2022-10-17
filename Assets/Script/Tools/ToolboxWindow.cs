using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToolboxWindow : EditorWindow
{
    [MenuItem("Toolbox/Toolbox Window")]
    static void InitWindow()
    {
        ToolboxWindow window = GetWindow<ToolboxWindow>();
        window.titleContent = new GUIContent("Toolbox");
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("bouton"))
        {
            Debug.Log("oui");
        }
    }
}
