using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor
{
    [HideInInspector] public MapManager mapManager = null;

    private void OnEnable()
    {
        this.mapManager = (MapManager)this.target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("When adding more map don't forget to add it here \n according to if it's interactible or not", MessageType.Info);
    }
}
