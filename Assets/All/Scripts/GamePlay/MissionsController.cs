using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JsonClass;
public class MissionsController : MonoBehaviour
{
    int level;
    int requireHostageRemain;
    int requireBulletRemain;
    List<CashReward> cashRewards;

    public void SetMissionInfo(int level)
    {
        var mission = GetMissionData(level);
        if(mission!= null)
        {
            SetMissionConfig(mission);
        }
    }

    public void SetMissionConfig(Level info)
    {
        level = info.level;
        requireBulletRemain = info.endGame.bullet;
        requireHostageRemain = info.endGame.hostage;
        cashRewards = info.endGame.cashReward;
    }

    public Level GetMissionData(int level)
    {
        var listMission = GameElement.Instance.gameDataController.GetGameData();

        foreach (var iter in listMission.Levels)
        {
            if(iter.level == level)
            {
                return iter;
            }
        }

        return null;
    }

    public int GetStarsResult(int remainEnemy, int remainBullet, int remainHostage)
    {
        if (remainEnemy > 0) return 0;
        int star = 3;
        star -= (remainBullet >= requireBulletRemain ? 0 : 1);
        star -= (remainBullet < 1) ? 1 : 0;

        return star;
    }

    public int GetReward(int remainEnemy, int remainBullet, int remainHostage, bool isWin = false)
    {
        if (!isWin) return cashRewards[0].star;
        int index = 0;
        if (remainEnemy <= 0) index++;
        if (remainBullet >= requireBulletRemain) index++;
        if(remainHostage >= requireHostageRemain) index++;

        return cashRewards[index].star;
    }
}
