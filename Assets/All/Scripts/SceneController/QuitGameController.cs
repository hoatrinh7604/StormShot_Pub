using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGameController : MonoBehaviour
{
    [SerializeField] Button btn;

    private void Start()
    {
        btn.onClick.AddListener(delegate { Quit(); });
    }

    public void Quit()
    {
        Application.Quit();
    }
}
