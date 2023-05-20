using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    [SerializeField] Slider loadingBar;
    [SerializeField] float delayTime = 4;
    private float timing = 0;
    [SerializeField] string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        timing = 0;
        loadingBar.maxValue = delayTime;
        loadingBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;
        loadingBar.value = timing;
        if(timing >= delayTime)
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        SceneManager.LoadSceneAsync(nextScene);
    }
}
