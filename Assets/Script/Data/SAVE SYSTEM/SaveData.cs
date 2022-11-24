using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using static SaveData;

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public struct GameData 
    {
        //public int profilCountTotal;

        //PLAYER STATS
        public int playerLVL;
        public int playerEXP;
        public int playerHP;
        public int playerMana;

        //PLAYER POSITIONN
        public float playerX;
        public float playerY;

        //PLAYER PROGRESS
        public int whatZone;
        
        public GameData(/*int profilCount, */int lvl, int exp, float posX, float posY, int hp, int mana, int zone = 93)
        {
            //profilCountTotal = profilCount;
            this.playerLVL = lvl;
            this.playerEXP = exp;
            this.playerX = posX;
            this.playerY = posY;
            this.playerHP = hp;
            this.playerMana = mana;
            this.whatZone = zone;
        }
    }
    public GameData MyGameData { get; private set; }


    [System.Serializable]
    public struct SettingData
    {
        public bool fullScreenOn;

        //public int idLanguage;
    
        //public int musicVolume;
        //public int sfxVolume;

        public SettingData(bool fullScreenOn/*, int idLanguage, int musicVolume, int sfxVolume*/)
        {
            this.fullScreenOn = fullScreenOn;
            /*this.idLanguage = idLanguage;
            this.musicVolume = musicVolume;
            this.sfxVolume = sfxVolume;*/
        }
    }
    public SettingData MySettingData { get; private set; }

    public SaveData() 
    {
        MyGameData = new GameData();
        MySettingData = new SettingData();
    }

    public SaveData(GameData gameData, SettingData settingData)
    {
        MyGameData = gameData;
        MySettingData = settingData;
    }
}
