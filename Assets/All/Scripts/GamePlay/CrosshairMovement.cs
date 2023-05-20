using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMovement : MonoBehaviour
{
    Vector3 pos;
    [SerializeField] float speed = 1f;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] LineShotController lineShotController;

    [SerializeField] bool showAtStart = true;
    private bool move = false;
    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        lineShotController = GetComponentInChildren<LineShotController>();

        ControlVisible(showAtStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            pos = Input.mousePosition;
            pos.z = speed;
            Vector3 vec = Camera.main.ScreenToWorldPoint(pos);
            transform.position = new Vector3(vec.x, vec.y, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ControlVisible(true);
        }
        else if (Input.GetMouseButton(0))
        {
            ControlVisible(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ControlVisible(false);
        }
    }

    //private void OnMouseDown()
    //{
    //    ControlVisible(true);
    //}

    //private void OnMouseDrag()
    //{
    //    ControlVisible(true);
    //}

    //private void OnMouseUp()
    //{
    //    ControlVisible(false);
    //}

    private void ControlVisible(bool isDisplay)
    {
        move = isDisplay;
        if (GameplayController.Instance.IsPauseGame())
        {
            spriteRenderer.enabled = false;
            lineShotController.ShowLine(false);
            return;
        }
        spriteRenderer.enabled = isDisplay;
        lineShotController.ShowLine(isDisplay);
    }
}
