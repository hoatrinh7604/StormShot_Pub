using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using JsonClass;

public class ItemButtonController : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] Sprite[] actionToggleSprites;
    [SerializeField] Image toggleIcon;
    [SerializeField] Image icon;

    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TextMeshProUGUI textQuantity;
    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] GameObject infoPopup;

    //string name;
    int quantity;
    int type;
    int price;
    string info;

    [SerializeField] Button addMoreButton;
    private MainMenuItemController mainMenuItemController;
    private InGameItemControl inGameItemControl;

    bool isSelected = false;
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate { OnClick(); });

        //mapLevelSceneController = FindAnyObjectByType<MapLevelSceneController>();
        mainMenuItemController = FindObjectOfType<MainMenuItemController>();
        inGameItemControl = FindObjectOfType<InGameItemControl>();
        addMoreButton.onClick.AddListener(delegate { AddMoreItem(); });
    }

    public void SetInfo(SupportItem item)
    {
        type = item.type;
        name = item.name;
        quantity = item.quantity;
        price = item.price;
        isSelected = item.isEquip == 1;
        info = item.info;

        textName.text = name;
        textQuantity.text = quantity.ToString();
        textPrice.text = price.ToString();
        infoPopup.GetComponentInChildren<TextMeshProUGUI>().text = info;

        if (quantity <= 0)
        {
            isSelected = false;
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }

        GetIcon(type);

        ToggleHandle();
    }

    public int GetItemType()
    {
        return type;
    }

    public int GetEquipState()
    {
        return isSelected ? 1: 0;
    }

    public void AddMoreItem()
    {
        if (mainMenuItemController)
        {
            mainMenuItemController.AddItem(UpdateItem, type, price);
        }
        else if (inGameItemControl)
        {
            inGameItemControl.AddItem(UpdateItem, type, price);
        }
    }

    private void UpdateItem()
    {
        quantity++;
        btn.interactable = true;
        textQuantity.text = quantity.ToString();
    }

    public void OnClick()
    {
        infoPopup.gameObject.GetComponent<ItemPopupInfo>().ShowInfo();

        if (quantity <= 0) return;

        isSelected = !isSelected;
        SetEquipment(type, isSelected ? 1 : 0);

        ToggleHandle();
    }

    public void SetEquipment(int type, int isEquip)
    {
        if (mainMenuItemController)
        {
            mainMenuItemController.EquipItem(type, isEquip);
        }
        else if(inGameItemControl)
        {
            inGameItemControl.EquipItem(type, isEquip);
        }
    }

    public bool IsEquip()
    {
        return isSelected;
    }

    public void ToggleHandle()
    {
        toggleIcon.sprite = actionToggleSprites[isSelected ? 1 : 0];
        toggleIcon.enabled = isSelected;
    }

    public void GetIcon(int type)
    {
        icon.sprite = Resources.Load<Sprite>("Items/ItemType"+ type);
    }
}
