using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleStartCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 basePosition;
    public Vector3 targetPosition;
    public float speed;

    bool moving = true;

    private void Start()
    {
        moving = true;
        cameraTransform.position = basePosition;
    }

    private void Update()
    {
        if(moving)
        {
            cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, targetPosition, speed * Time.deltaTime);

            if(Vector3.Distance(cameraTransform.position, targetPosition) <= 0.01f)
            {
                cameraTransform.position = targetPosition;
                moving = false;
            }
        }
    }
}
