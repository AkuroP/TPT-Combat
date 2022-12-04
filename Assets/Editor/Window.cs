using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Window : EditorWindow
{
    public Object spriteChange;

    
    [MenuItem("Window/ChangeSprite")]
    public static void OpenWindow()
    {
        GetWindow<Window>("ChangeSprite");
    }
    void OnGUI()
    {
        GUILayout.Label("Change the sprite of the item", EditorStyles.boldLabel);

        spriteChange = EditorGUILayout.ObjectField(spriteChange, typeof(Sprite), false, GUILayout.Width(64), GUILayout.Height(64));

        if (GUILayout.Button("ChangeSprite"))
        {
           foreach(GameObject obj in Selection.gameObjects)
            {
                SpriteRenderer render =  obj.GetComponent<SpriteRenderer>();
                if(render != null)
                {
                    render.sprite = (Sprite)spriteChange;
                }
            }
            Debug.Log("SpriteChanged");
        }
    }
}
