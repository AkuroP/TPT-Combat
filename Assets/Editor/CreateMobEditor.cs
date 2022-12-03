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
    
    private ScrollView DetailSection;
    private VisualElement LargeDisplayIcon;
    private EntityData activeEntity;

    [MenuItem("Toolbox/Create Mob")]
    public static void Init()
    {
        //Create the window
        CreateMobEditor wnd = GetWindow<CreateMobEditor>();
        
        //Set the title of the window
        wnd.titleContent = new GUIContent("Mob Database");
        
        //Set the size of the window
        Vector2 size = new Vector2(1000, 475);
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
        entityTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/MobRowTemplate.uxml");
        
        DefaultSprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Image/UnknownIcon.png", typeof(Sprite));

        //Load all entity asset
        LoadAllMobs();
        
        
        entityTab = rootVisualElement.Q<VisualElement>("EntityTab");
        GenerateListView();

        DetailSection = rootVisualElement.Q<ScrollView>("ScrollView_Details");
        
        LargeDisplayIcon = DetailSection.Q<VisualElement>("Icon");

        rootVisualElement.Q<Button>("Btn_AddEntity").clicked += AddEntityOnClick;
        rootVisualElement.Q<Button>("Btn_DeleteEntity").clicked += Delete_OnClick;

        
        DetailSection.Q<TextField>("EntityName").RegisterValueChangedCallback(evt =>
        { 
            activeEntity._Name = evt.newValue;
            entityListView.Rebuild();
        });
        
        DetailSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        { 
            activeEntity.Description = evt.newValue;
            entityListView.Rebuild();
        });

        DetailSection.Q<ObjectField>("IconPicker").RegisterValueChangedCallback(evt =>
        {
            Sprite newSprite = evt.newValue as Sprite;
            activeEntity.Icon = newSprite == null ? DefaultSprite : newSprite;
            LargeDisplayIcon.style.backgroundImage = newSprite == null ? DefaultSprite.texture : newSprite.texture;
            
            entityListView.Rebuild();
        });

        #region TypeEntity

            DetailSection.Q<Toggle>("AnEnemy").RegisterValueChangedCallback(evt =>
            { 
                activeEntity._anEnemy = evt.newValue;
                entityListView.Rebuild();
            });
            
            DetailSection.Q<IntegerField>("EntityExp").RegisterValueChangedCallback(evt =>
            { 
                activeEntity._expValue = evt.newValue;
                entityListView.Rebuild();
            });

        #endregion

        #region Stat

            DetailSection.Q<IntegerField>("EntityHP").RegisterValueChangedCallback(evt =>
            { 
                activeEntity._hp = evt.newValue;
                entityListView.Rebuild();
            });
            
            DetailSection.Q<IntegerField>("EntityHP").RegisterValueChangedCallback(evt =>
            { 
                activeEntity._hp = evt.newValue;
                entityListView.Rebuild();
            });
            
            DetailSection.Q<IntegerField>("EntityAtk").RegisterValueChangedCallback(evt =>
            { 
                activeEntity._atk = evt.newValue;
                entityListView.Rebuild();
            });
            
            DetailSection.Q<IntegerField>("EntitySATK").RegisterValueChangedCallback(evt =>
            { 
                activeEntity._sAtk = evt.newValue;
                entityListView.Rebuild();
            });
            
            DetailSection.Q<IntegerField>("EntityDef").RegisterValueChangedCallback(evt =>
            { 
                activeEntity._def = evt.newValue;
                entityListView.Rebuild();
            });
            
            DetailSection.Q<IntegerField>("EntitySDef").RegisterValueChangedCallback(evt =>
            { 
                activeEntity._sDef = evt.newValue;
                entityListView.Rebuild();
            });
            
            DetailSection.Q<IntegerField>("EntitySpeed").RegisterValueChangedCallback(evt =>
            { 
                activeEntity._speed = evt.newValue;
                entityListView.Rebuild();
            });

        #endregion

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
        //Defining what each entity will visually look like. In this case, the makeEntity function is creating a clone of the EntityRowTemplate.
        Func<VisualElement> makeEntity = () => entityTemplate.CloneTree();

        //Define the binding of each individual Entity that is created. Specifically, 
        //it binds the Icon visual element to the scriptable object’s Icon property and the 
        //Name label to the Name property.
        Action<VisualElement, int> bindEntity = (e, i) =>
        {
            e.Q<VisualElement>("Icon").style.backgroundImage = entityDataBase[i] == null ? DefaultSprite.texture :  entityDataBase[i].Icon.texture;
            e.Q<Label>("Name").text = entityDataBase[i]._Name;
        };

        //Create the listview and set various properties
        entityListView = new ListView(entityDataBase, entityHeight, makeEntity, bindEntity);
        entityListView.selectionType = SelectionType.Single;
        entityListView.style.height = entityDataBase.Count * entityHeight + 5;
        entityTab.Add(entityListView);

        entityListView.onSelectionChange += ListView_onSelectionChange;
        
    }

    private void ListView_onSelectionChange(IEnumerable<object> selectedEntities)
    {
        //Get the first entity in the selectedEntity list. 
        //There will only ever be one because SelectionType is set to Single
        activeEntity = (EntityData)selectedEntities.First();

        //Create a new SerializedObject and bind the Details VE to it. 
        //This cascades the binding to the children
        SerializedObject so = new SerializedObject(activeEntity);
        DetailSection.Bind(so);

        //Set the icon if it exists
        if (activeEntity.Icon != null)
        {
            LargeDisplayIcon.style.backgroundImage = activeEntity.Icon.texture;
        }

        //Make sure the detail section is visible. This can turn off when you delete an entity
        DetailSection.style.visibility = Visibility.Visible;
    }

    private void AddEntityOnClick()
    {
        //Create an instance of the scriptable object and set the default parameters
        EntityData newEntity = CreateInstance<EntityData>();
        newEntity._Name = $"New Entity";
        newEntity.Icon = DefaultSprite;
        
        //Create the asset, using the unique ID for the name
        AssetDatabase.CreateAsset(newEntity, $"Assets/Scriptable/{newEntity.GetInstanceID()}.asset");
        
        //Add it to the list
        entityDataBase.Add(newEntity);
        
        //refresh the list view for all redraw again
        entityListView.Rebuild();
        entityListView.style.height = entityDataBase.Count * entityHeight;

    }

    private void Delete_OnClick()
    {
        //Get the path of the fie and delete it through AssetDatabase
        string path = AssetDatabase.GetAssetPath(activeEntity);
        AssetDatabase.DeleteAsset(path);
        
        //Purge the reference frm the list and refresh the list view
        entityDataBase.Remove(activeEntity);
        entityListView.Rebuild();
        
        //if nothing is selected, hide the details section
        DetailSection.style.visibility = Visibility.Hidden;
    }
  
    
}
