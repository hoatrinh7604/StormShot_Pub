using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchSceneByButton : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] int sceneIndex;

    [SerializeField] Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate { GoToScene(); });
    }

    public void GoToScene()
    {
        if (sceneName != "")
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        }
    }
}
