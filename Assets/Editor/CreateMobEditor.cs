using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

public class CreateMobEditor : EditorWindow
{
    private Sprite DefaultSprite;
    private static List<EntityData> entityDataBase= new List<EntityData>();
    
    private VisualElement entityTab;
    private static VisualTreeAsset entityTemplate;
    private ListView entityListView;
    private float entityHeight = 10;

    [MenuItem("Toolbox/Create Mob")]
    public static void Init()
    {
        //Create the window
        CreateMobEditor wnd = GetWindow<CreateMobEditor>();
        
        //Set the title of the window
        wnd.titleContent = new GUIContent("Mob Database");
        
        //Set the size of the window
        Vector2 size = new Vector2(800, 475);
        wnd.minSize = size;
        wnd.maxSize = size;
    }
    public void CreateGUI()
    {
        //Load the uxml and uss to add them to the root visual of the window
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/MobDataBase.uxml");
        VisualElement rootFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(rootFromUXML);
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/MobDataBase.uss");
        rootVisualElement.styleSheets.Add(styleSheet);
        
        //Import the ListView Entity Template
        entityTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/WUG/Editor/ItemRowTemplate.uxml");
        DefaultSprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Image/UnknownIcon.png", typeof(Sprite));

        //Load all entity asset
        LoadAllMobs();
        
        
        entityTab = rootVisualElement.Q<VisualElement>("EntityTab");
        GenerateListView();
    }

    //Look through all entity located in Assets/Script/BattleRelated/Entity/Scriptable and load them into memory
    private void LoadAllMobs()
    {
        entityDataBase.Clear();
        string[] allPaths = Directory.GetFiles("Assets/Scriptable", "*.asset", SearchOption.AllDirectories);

        //Correct the slashes in the path and adds a ref to the entity to the list
        foreach (string path in allPaths)
        {
            string cleanedPath = path.Replace("\\", "/");
            entityDataBase.Add((EntityData)AssetDatabase.LoadAssetAtPath(cleanedPath, typeof(EntityData)));
        }
    }

    private void GenerateListView()
    {
        //Definie the visual of each entity, here the makeEntity clone the entitytemplate
        Func<VisualElement> makeEntity = () => entityTemplate.CloneTree();

        //Bind each individual entity that is created. Bind icon and Name with scriptable object's icon and name
        Action<VisualElement, int> bindEntity = (e, i) =>
        {
            e.Q<VisualElement>("Icon").style.backgroundImage = entityDataBase[i] == null ? DefaultSprite.texture :  entityDataBase[i].sprite.texture;
            e.Q<Label>("Name").text = entityDataBase[i]._Name;
        };

        //Create the listview and set various properties
        entityListView = new ListView(entityDataBase, entityHeight, makeEntity, bindEntity);
        entityListView.selectionType = SelectionType.Single;
        entityListView.style.height = entityDataBase.Count * entityHeight + 5;
        entityTab.Add(entityListView);
    
    }
  
    
}
