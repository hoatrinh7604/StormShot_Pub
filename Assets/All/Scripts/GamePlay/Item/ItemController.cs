using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using JsonClass;
public class ItemController : MonoBehaviour
{
    List<SupportItem> listItems = new List<SupportItem>();
    //[SerializeField] int quantity = 0;

    [SerializeField] Transform parentGroupItems;
    [SerializeField] GameObject itemPrefab;
    List<ItemButtonController> listButtonItem = new List<ItemButtonController>();
    [SerializeField] GameDataController gameDataController;

    private void Start()
    {
        gameDataController = FindObjectOfType<GameDataController>();
    }

    public void UpdateUserItems()
    {
        foreach(var item in listItems)
        {
            var i = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, parentGroupItems);
            i.GetComponent<ItemButtonController>().SetInfo(item);
            listButtonItem.Add(i.GetComponent<ItemButtonController>());
        }
    }

    public void UsingItem()
    {
        //foreach (var item in listButtonItem)
        //{
        //    int type = item.GetType();

        //    if(item.IsEquip())
        //    {
        //        gameDataController.AddMoreItemByType(type, -1);
        //        //gameDataController.UpdateEquipmentState(type, item.GetEquipState());
        //    }
        //}
    }

    public void GoGamePlay()
    {
        UsingItem();
    }

    public void SetListItem(List<SupportItem> list)
    {
        listItems = list;
    }

    public void ReloadData(List<SupportItem> list)
    {
        for(int i = 0; i < listButtonItem.Count; i++)
        {
            listButtonItem[i].GetComponent<ItemButtonController>().SetInfo(list[i]);
        }
    }
}
