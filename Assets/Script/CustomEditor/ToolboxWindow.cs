#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class ToolboxWindow : EditorWindow
{
    [MenuItem("Toolbox/Tool Box")]
    static void Initbox()
    {
        ToolboxWindow newWindow = GetWindow<ToolboxWindow>();
        
        newWindow.titleContent = new GUIContent("Tool Box");

        newWindow.Show();
    }


    private string scenePath = "Assets/Scenes/";
    private string entitiesPath = "Script/BattleRelated/Entity/Scriptable/";
    private Vector2 scrollPos;
    [SerializeField] private List<GameObject> explorationMaps;
    private void OnGUI()
    {
        if(!EditorApplication.isPlaying)
        {
            //Scenes TP
            scenePath = EditorGUILayout.TextField("Path mark last / in the end", scenePath);
            //if(GUILayout.Button("Search"))
            //{
                    //verify if Directory exist
                    if(System.IO.Directory.Exists(scenePath))
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("GO TO :");

                        //get all scenes
                        string[] allScene = Directory.GetFiles(scenePath, "*.unity");
                        if(allScene.Length > 0)
                        {

                            foreach(string file in allScene)
                            {
                                //Debug.Log(Path.GetFileName(file));
                                string sceneName = Path.GetFileName(file);
                                
                                Scene scene;
                                if(!EditorSceneManager.GetSceneByPath(scenePath + sceneName).isSubScene && !EditorSceneManager.GetSceneByPath(scenePath + sceneName).IsValid())
                                {
                                    //Debug.Log(removedExtension);
                                    scene = EditorSceneManager.GetSceneByName(NameWithoutExt(sceneName, ".unity"));
                                }
                                else
                                {
                                    if(SceneManager.sceneCount < allScene.Length)scene = EditorSceneManager.OpenScene(scenePath + sceneName, OpenSceneMode.AdditiveWithoutLoading);
                                    else scene = EditorSceneManager.GetSceneByName(NameWithoutExt(sceneName, ".unity"));
                                    //Debug.Log(scene.name);
                                }
                                //Debug.Log(sceneName);
                                bool sceneButton = GUILayout.Button(scene.name);
                                //Open scene if not already open
                                if(sceneButton)
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
                        else
                        {
                            EditorGUILayout.HelpBox($"No scenes currently in path {scenePath}", MessageType.Warning);
                            GUILayout.EndHorizontal();
                        }
                    }
                    else
                    {
                        EditorGUILayout.HelpBox($"No path existing in path {scenePath}", MessageType.Warning);
                    }  
            //}

            //Entity list
            entitiesPath = EditorGUILayout.TextField("Entities Paths (end with a '/')", entitiesPath);
            if(entitiesPath.EndsWith("/"))
            {
                FileInfo[] allEntitiesInfo = new DirectoryInfo(Application.dataPath + "/" + entitiesPath).GetFiles("*.asset");
                scrollPos = GUILayout.BeginScrollView(scrollPos,true, false);
                GUILayout.BeginHorizontal();
                for(int i = 0; i < allEntitiesInfo.Length; i++)
                {
                    if(GUILayout.Button(allEntitiesInfo[i].Name))
                    {
                        
                        //affiche une fenêtre avec les stats de l'entite
                        //EntityData entity = allEntitiesInfo[i] as entitiesPath;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.EndScrollView();

            }

            //Exploration maps
            //EditorGUILayout.LabelField("All Exploration Maps :", explorationMaps);

            //Change map
            SerializedObject so = new SerializedObject(this);
            if(GameObject.FindGameObjectWithTag("GM") != null)
            {
                explorationMaps = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().allZone;
                
                SerializedProperty allExplorMaps = so.FindProperty("explorationMaps");
                EditorGUILayout.PropertyField(allExplorMaps);
                so.ApplyModifiedProperties();

                GUILayout.BeginHorizontal();
                for(int i = 0; i < explorationMaps.Count; i++)
                {
                    if(GUILayout.Button(explorationMaps[i].name))
                    {
                        foreach(GameObject map in explorationMaps)
                        {
                            if(map != explorationMaps[i])map.SetActive(false);
                            else explorationMaps[i].SetActive(true);
                        }
                        
                    }
                }
                GUILayout.EndHorizontal();

            }

            //open player prefab
            if(GUILayout.Button("Open Player Prefab"))
            {
                AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath("Assets/Player/Player.prefab"));
            }

        }
        else
        {
            //Reload Game
            if(GUILayout.Button("Reload Game"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    //Remove extention of name
    private string NameWithoutExt(string name, string extName)
    {
        string copy = name;
        string removedExtension = copy.Replace(extName, "");
        return removedExtension;
    }
}

#endif
