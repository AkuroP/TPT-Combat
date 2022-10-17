using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterCreationWindow : EditorWindow
{
    [MenuItem("Toolbox/Toolbox Window")]
    static void InitWindow()
    {
        CharacterCreationWindow window = GetWindow<CharacterCreationWindow>();
        window.titleContent = new GUIContent("Create character");
        window.Show();
    }

    void OnGUI()
    {
        GUI.backgroundColor = new Color(0.8f, 0.4f, 0, 1);

        if (GUILayout.Button("bouton"))
        {
            Debug.Log("oui");
        }
    }
}
