using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class ToolboxWindow : EditorWindow
{
    [MenuItem("Toolbox/Scene Loader")]
    static void Initbox()
    {
        ToolboxWindow newWindow = GetWindow<ToolboxWindow>();
        
        newWindow.titleContent = new GUIContent("Scene Loader");

        newWindow.Show();
    }


    

    private void OnGUI()
    {
        if(!EditorApplication.isPlaying)
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("GO TO :");
            string scenePath = Application.dataPath + "/Scenes/";
            string[] allScene = Directory.GetFiles(scenePath, "*.unity");
            
            foreach(string file in allScene)
            {
                //Debug.Log(Path.GetFileName(file));
                string sceneName = Path.GetFileName(file);
                Scene scene = EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName, OpenSceneMode.AdditiveWithoutLoading);
                //Debug.Log(sceneName);
                if(GUILayout.Button(scene.name))
                {
                    if(EditorSceneManager.GetActiveScene() != EditorSceneManager.GetSceneByName(sceneName))
                    {
                        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                        EditorSceneManager.OpenScene(scenePath + sceneName, OpenSceneMode.Single);
                    }
                    else Debug.LogError($"{SceneManager.GetActiveScene().name} SCENE ALREADY OPEN");
                }
                
            }
            GUILayout.EndHorizontal();
        }
    }
}
