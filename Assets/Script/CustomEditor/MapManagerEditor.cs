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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tileBase"), new GUIContent("Tile Base", "The base of Colored tiles"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mapState"), new GUIContent($"{this.mapManager.mapState} maps", $"Map that is {this.mapManager.mapState} to the player"));

        EditorGUILayout.BeginHorizontal();
        switch(mapManager.mapState)
        {
            case MapManager.MapState.ACCESSIBLE :
                EditorGUILayout.PropertyField(serializedObject.FindProperty("accessibleMap"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("mm_accessible"));
            break;

            case MapManager.MapState.UNACESSIBLE :
                EditorGUILayout.PropertyField(serializedObject.FindProperty("unaccessibleMap"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("mm_unaccessible"));
            break;

            case MapManager.MapState.INTERACTIBLE :
                EditorGUILayout.PropertyField(serializedObject.FindProperty("interactibleMap"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("mm_interactible"));
            break;

            case MapManager.MapState.PATH :
                EditorGUILayout.PropertyField(serializedObject.FindProperty("pathMap"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("mm_path"));
            break;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.HelpBox($"Don't forget to add new {this.mapManager.mapState} maps here !", MessageType.Info);

        serializedObject.ApplyModifiedProperties();
    }
}
