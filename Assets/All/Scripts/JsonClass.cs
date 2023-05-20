using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    #region Game data
    [System.Serializable]
    public class CashReward
    {
        public int star;
    }
    [System.Serializable]
    public class EndGame
    {
        public int bullet;
        public int hostage;
        public List<CashReward> cashReward;
    }
    [System.Serializable]
    public class Level
    {
        public int level;
        public bool isUnlock;
        public int star;
        public int bullet;
        public EndGame endGame;
    }
    [System.Serializable]
    public class GameData
    {
        public int Cash;
        public List<SupportItem> SupportItems;
        public List<Level> Levels;
    }
    [System.Serializable]
    public class SupportItem
    {
        public string name;
        public int type;
        public int quantity;
        public int price;
        public int isEquip;
    }
    #endregion
}
