using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] Slider soundVolume;
    [SerializeField] Slider effectVolume;
    [SerializeField] Button vibrationBtn;
    [SerializeField] Button policyBtn;
    [SerializeField] Button termOfUseBtn;

    private void Start()
    {
        soundVolume.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        soundVolume.minValue = 0;
        soundVolume.value = PlayerPrefs.GetFloat("Volume", 0.3f);
        soundVolume.maxValue = 1;

        effectVolume.onValueChanged.AddListener(delegate { OnSliderEffectValueChanged(); });
        effectVolume.minValue = 0;
        effectVolume.value = PlayerPrefs.GetFloat("EffectSound", 1);
        effectVolume.maxValue = 1;

        policyBtn.onClick.AddListener(Policy);
        termOfUseBtn.onClick.AddListener(TermOfUse);
        //vibrationBtn.onClick.AddListener(HandleVibrate);
        //vibrationBtn.GetComponent<ToggleButton>().EnableBtn(PlayerPrefs.GetInt("Vibrate", 0) == 1 ? true : false);
    }

    public void OnSliderValueChanged()
    {
        SoundController.Instance.ChangeVolume(soundVolume.value);
        PlayerPrefs.SetFloat("Volume", soundVolume.value);
    }

    public void OnSliderEffectValueChanged()
    {
        PlayerPrefs.SetFloat("EffectSound", effectVolume.value);
    }

    public void HandleVibrate()
    {
        bool isEnable = vibrationBtn.GetComponent<ToggleButton>().IsEnable();
        PlayerPrefs.SetInt("Vibrate", isEnable ? 1 : 0);
    }

    public void Policy()
    {
        Application.OpenURL("https://policy.monsterstudio.io");
    }

    public void TermOfUse()
    {
        Application.OpenURL("https://term-of-use.monsterstudio.io");
    }
}
