using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public static class SaveSystem
{
    public static void InitSaveData()
    {
        string path = Application.persistentDataPath + "/Game.save";
        
        if (File.Exists(path))
            return;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, new SaveData());
        stream.Close();

        Debug.Log("Init save successful");
    }

    #region GAME
    public static void SaveGameData(int lvl, int exp, float posX, float posY, int hp, int mana, int zone)
    {
        SaveData.GameData saveGame = new SaveData.GameData(lvl, exp, posX, posY, hp, mana, zone);
        SaveData data = new SaveData(saveGame, LoadSettingData());

        string path = Application.persistentDataPath + "/Game.save";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Save game successful");
    }

    public static SaveData.GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/Game.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Load game successful");

            //DEBUG ALL PARAMETER
            /*Debug.Log("player lvl : " + data.MyGameData.playerLVL);
            Debug.Log("player XP : " + data.MyGameData.playerEXP);
            Debug.Log("player POS : " + data.MyGameData.playerX + " ; " + data.MyGameData.playerY);
            Debug.Log("Player HP :" + data.MyGameData.playerHP);
            Debug.Log("Player Mana : " + data.MyGameData.playerMana);
            Debug.Log("Player Zone : " + data.MyGameData.whatZone);*/



            return data.MyGameData;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            ResetGameData();
            return LoadGameData();
        }
    }

    public static void ResetGameData()
    {
        SaveData data = new SaveData(new SaveData.GameData(), LoadSettingData());

        string path = Application.persistentDataPath + "/Game.save";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
        
        Debug.Log("Reset game successful");
    }

    #endregion

    #region SETTING
    public static void SaveSettingData()
    {
        SaveData.SettingData saveSetting = new SaveData.SettingData();
        SaveData data = new SaveData(LoadGameData(), saveSetting);

        string path = Application.persistentDataPath + "/Game.save";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Save setting successful");
    }

    public static SaveData.SettingData LoadSettingData()
    {
        string path = Application.persistentDataPath + "/Game.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Load setting successful");

            return data.MySettingData;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            ResetSettingData();
            return LoadSettingData();
        }
    }

    public static void ResetSettingData()
    {
        SaveData data = new SaveData(LoadGameData(), new SaveData.SettingData());

        string path = Application.persistentDataPath + "/Game.save";
        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
        
        Debug.Log("Reset setting successful");
    }

    #endregion

    /*#if UNITY_EDITOR
    public static int GetLocalId(Object obj)
    {
        PropertyInfo inspectorModeInfo =
        typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

        SerializedObject serializedObject = new SerializedObject(obj);
        inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);

        SerializedProperty localIdProp =
            serializedObject.FindProperty("m_LocalIdentfierInFile");   //note the misspelling!

        return localIdProp.intValue;
    }
    #endif*/
}
