using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] GameObject pauseState;

    [SerializeField] Sprite[] pauseStateSprites;
    private bool isPause = false;
    int UILayer;
    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
        pauseButton.onClick.AddListener(PauseControl);
    }

    public void PauseControl()
    {
        isPause = !isPause;
        Pause(isPause);
    }

    public void Pause(bool pause, int scaleTime = 1)
    {
        isPause = pause;
        pauseButton.image.sprite = pauseStateSprites[isPause ? 1 : 0];
        pauseState.SetActive(isPause);

        
        if(isPause)
        {
            Time.timeScale = 0;
            
        }
        else
        {
            Time.timeScale = 1;
            //StartCoroutine(ReturnToPlay());
        }
        GameplayController.Instance.PauseControl(isPause);
    }

    IEnumerator ReturnToPlay()
    {
        yield return new WaitForSeconds(0.2f);
        GameplayController.Instance.PauseControl(false);
    }

    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
            {
                return true;
            }
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    public static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
