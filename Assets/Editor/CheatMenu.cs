using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheatMenu : EditorWindow
{
    [MenuItem("Tools/CheatMenu")]
    public static void CreateWindows()
    {
        EditorWindow.GetWindow<CheatMenu>();
    }

    public void OnGUI()
    {
        
    }
}
