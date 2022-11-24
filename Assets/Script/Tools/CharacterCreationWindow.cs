using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterCreationWindow : EditorWindow
{
    SerializedObject serializedObject;
    SerializedProperty currentProperty;
    Object entityData;
    

    [MenuItem("Toolbox/Character Creation")]
    static void InitWindow()
    {
        CharacterCreationWindow window = GetWindow<CharacterCreationWindow>();
        window.titleContent = new GUIContent("Create character");
        //window.serializedObject = new SerializedObject();
        window.Show();
    }

    public Vector2 scrollPosition = Vector2.zero;

    void Start()
    {

    }

    void OnGUI()
    {
        GUI.backgroundColor = new Color(1f, .8f, 0.8f, 1);
        
        entityData = EditorGUILayout.ObjectField(entityData, typeof(SerializedObject), true);
        currentProperty = serializedObject.FindProperty("entityData");
        DrawProperties(currentProperty, true);
        
        GUI.EndScrollView();
    }

    void DrawProperties(SerializedProperty prop, bool drawChildren)
    {
        string lastPropPath = string.Empty;
        foreach(SerializedProperty p in prop)
        {
            if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                EditorGUILayout.EndHorizontal();

                if (p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath)){continue;}
                lastPropPath = p.propertyPath;
                EditorGUILayout.PropertyField(p, drawChildren);
            }
        }
    }
}
