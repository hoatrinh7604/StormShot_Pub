using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public void DoubleRewards()
    {
        AdsInitializer.Instance.rewardedADs.ShowRewarded(OnRewardSuccess, OnRewardFail);
    }

    public void OnRewardSuccess()
    {
        Debug.Log("Get rewarded");
    }

    public void OnRewardFail()
    {
        Debug.Log("Fail to get rewarded");
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger enter");
    }
}
