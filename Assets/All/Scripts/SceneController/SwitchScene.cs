using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] bool isTapToNext;

    [SerializeField] bool isCountdownTimeToNext;
    [SerializeField] float timeToNext;

    [SerializeField] string sceneName;
    [SerializeField] int sceneIndex;

    // Update is called once per frame
    void Update()
    {
        if(isTapToNext)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GoToScene();
            }
        }
        else if(isCountdownTimeToNext)
        {
            timeToNext -= Time.deltaTime;
            if (timeToNext < 0)
            {
                GoToScene();
            }
        }
    }

    public void GoToScene()
    {
        if(sceneName != "")
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
