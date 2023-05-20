using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonClass;

public class LevelInfoController : MonoBehaviour
{
    [SerializeField] GameObject[] allLevelPos;
    [SerializeField] GameObject levelInfoPrefab;
    [SerializeField] Sprite[] levelSprites;
    public void SetAllLevelInfo(List<Level> listLevel)
    {
        for(int i = 0; i < listLevel.Count; i++)
        {
            var item = Instantiate(levelInfoPrefab, Vector3.zero, Quaternion.identity, allLevelPos[i].transform);
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<LevelInfoItem>().SetInfo(listLevel[i], levelSprites[Random.Range(0, levelSprites.Length)]);
        }
    }
}
