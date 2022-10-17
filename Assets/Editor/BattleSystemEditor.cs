using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BattleSystem))]
public class BattleSystemEditor : Editor
{
    // Object battleSystem, playerSprite, enemySprite;
    // public override void OnInspectorGUI()
    // {
    //     serializedObject.Update();

    //     SerializedProperty charaBattle = serializedObject.FindProperty("characterBattle");

    //     battleSystem = EditorGUILayout.ObjectField("Character Battle", battleSystem, typeof(Transform), true);
    //     charaBattle = battleSystem.;
    //     EditorGUILayout.Space();
    //     playerSprite = EditorGUILayout.ObjectField("Player Sprite", playerSprite, typeof(Sprite), true);
    //     EditorGUILayout.Space();
    //     enemySprite = EditorGUILayout.ObjectField("Enemy Sprite", enemySprite, typeof(Sprite), true);
    //     EditorGUILayout.Space();
    //     EditorGUILayout.HelpBox("Attention chien méchant", MessageType.Warning);
    //     EditorGUILayout.Space();
    //     EditorGUILayout.LabelField("bottom text");

    //     serializedObject.ApplyModifiedProperties();
    // }
}
