using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{

    [SerializeField] Sprite[] actionSprite;
    [Tooltip("Has only 2 element: The first element is enable - 0; the second is disable - 1")]

    private Button btn;
    [SerializeField] bool isEnable = false;

    // Start is called before the first frame update
    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate { SwitchEnable(); });

        SetUpBtn();
    }


    public void SetUpBtn()
    {
        btn.image.sprite = actionSprite[isEnable ? 0 : 1];
    }

    public void SwitchEnable()
    {
        isEnable = !isEnable;
        btn.image.sprite = actionSprite[isEnable ? 0 : 1];
    }

    public void EnableBtn(bool isEn)
    {
        isEnable = isEn;
        SetUpBtn();
    }

    public bool IsEnable()
    {
        return isEnable;
    }
}
