using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JsonClass;
public class GameDataController : MonoBehaviour
{
    [SerializeField] DataSaveController dataSaveController;
    [SerializeField] TextAsset baseData;

    private GameData gameData;

    private void Awake()
    {
        gameData = GetGameData();
    }

    public void AddMoreItemByType(int type, int quantity)
    {
        UpdateItem(type, quantity);
    }

    public void AddMoreCash(int moreCash)
    {
        UpdateCash(moreCash);
    }

    public void UpdateEquipmentState(int type, int isEquip)
    {
        UpdateEquipment(type, isEquip);
    }

    public void UpdateEquipmentListState(List<SupportItem> list)
    {
        UpdateEquipmentForMoreItems(list);
    }

    public GameData GetGameData()
    {
        if (gameData == null)
        {
            var dataLocal = dataSaveController.ReadFile();
            var data = new GameData();

            if (dataLocal != "")
            {
                data = JsonUtility.FromJson<GameData>(dataLocal);
                gameData = data;
            }
        }

        return gameData;
    }

    public void ReloadGameData()
    {
        var dataLocal = dataSaveController.ReadFile();
        var data = new GameData();

        if (dataLocal != "")
        {
            data = JsonUtility.FromJson<GameData>(dataLocal);
            gameData = data;
        }
    }

    private void UpdateItem(int type, int quantity)
    {
        var dataLocal = dataSaveController.ReadFile();
        var data = new GameData();

        if(dataLocal != "")
        {
            data = JsonUtility.FromJson<GameData>(dataLocal);
        }

        if (data.SupportItems == null) return;

        foreach(var item in data.SupportItems)
        {
            if(item.type == type)
            {
                item.quantity += quantity;
                break;
            }
        }

        dataSaveController.WriteFile(JsonUtility.ToJson(data));
        gameData = data;
    }

    private void UpdateEquipment(int type, int isEquip)
    {
        var dataLocal = dataSaveController.ReadFile();
        var data = new GameData();

        if (dataLocal != "")
        {
            data = JsonUtility.FromJson<GameData>(dataLocal);
        }

        if (data.SupportItems == null) return;

        foreach (var item in data.SupportItems)
        {
            if (item.type == type)
            {
                item.isEquip = isEquip;
                break;
            }
        }

        dataSaveController.WriteFile(JsonUtility.ToJson(data));
        gameData = data;
    }

    private void UpdateEquipmentForMoreItems(List<SupportItem> listItems)
    {
        var dataLocal = dataSaveController.ReadFile();
        var data = new GameData();

        if (dataLocal != "")
        {
            data = JsonUtility.FromJson<GameData>(dataLocal);
        }

        if (data.SupportItems == null) return;

        data.SupportItems.Clear();
        data.SupportItems.AddRange(listItems);

        dataSaveController.WriteFile(JsonUtility.ToJson(data));
        gameData = data;
    }

    private void UpdateCash(int moreCash)
    {
        var dataLocal = dataSaveController.ReadFile();
        var data = new GameData();

        if (dataLocal != "")
        {
            data = JsonUtility.FromJson<GameData>(dataLocal);
        }

        if (data == null) return;

        data.Cash += moreCash;

        dataSaveController.WriteFile(JsonUtility.ToJson(data));
        gameData = data;
    }

    public void CompletedLevel(int level, int star, int cashReward)
    {
        var dataLocal = dataSaveController.ReadFile();
        var data = new GameData();

        if (dataLocal != "")
        {
            data = JsonUtility.FromJson<GameData>(dataLocal);
        }

        if (data == null) return;

        data.Cash += cashReward;

        for(int i = 0; i< data.Levels.Count; i++)
        {
            if (data.Levels[i].level == level)
            {
                data.Levels[i].star = star;

                // Unlock nextlevel
                if (star > 0 && level < data.Levels.Count)
                {
                    data.Levels[i + 1].isUnlock = true;
                }
                break;
            }
        }


        dataSaveController.WriteFile(JsonUtility.ToJson(data));
        gameData = data;
    }

    private void FirstRunSetup()
    {
        if(PlayerPrefs.GetInt("FirstRun") == 0)
        {
            dataSaveController.WriteFile(baseData.ToString());
            PlayerPrefs.SetInt("FirstRun", 1);
        }
    }
}
