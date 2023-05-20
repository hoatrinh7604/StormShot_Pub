using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using JsonClass;

public class LevelInfoItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] GameObject lockLabel;
    [SerializeField] GameObject starGroup;
    [SerializeField] Image[] starsList;
    [SerializeField] Button goPlayButton;

    [SerializeField] float darkColor;
    [SerializeField] MapLevelSceneController mapLevelSceneController;
    private int levelNumber;

    private void Awake()
    {
        mapLevelSceneController = FindObjectOfType<MapLevelSceneController>();
        goPlayButton.onClick.AddListener(PlayLevel);
    }

    public void SetInfo(Level level, Sprite bg)
    {
        goPlayButton.image.sprite = bg;
        levelNumber = level.level;
        textLevel.text = levelNumber.ToString();
        SetLock(level.isUnlock);
        SetStars(level.star);
        goPlayButton.interactable = level.isUnlock;
    }

    public void PlayLevel()
    {
        PlayerPrefs.SetInt("ChoosenLevel", levelNumber);
        mapLevelSceneController.GoGamePlay();
    }

    private void SetLock(bool isUnLock)
    {
        lockLabel.SetActive(!isUnLock);
        starGroup.SetActive(isUnLock);
    }

    private void SetStars(int starRate)
    {
        for(int i = 0; i< starsList.Length; i++)
        {
            if (i < starRate) continue;
            SetDark(i);
        }
    }

    private void SetDark(int index)
    {
        var color = starsList[index].color;
        starsList[index].color = new Color(color.r, color.g, color.b, darkColor);
    }
}
