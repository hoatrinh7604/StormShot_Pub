using Unity.Services.Mediation.Samples;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.InputSystem.XR;

public class AdsInitializer : MonoBehaviour
{
    public static AdsInitializer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public InterstitialExample interstitialADs;
    public RewardedExample rewardedADs;
    public BannerExample banner;
}