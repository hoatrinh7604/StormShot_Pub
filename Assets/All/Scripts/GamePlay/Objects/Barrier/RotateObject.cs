using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 force;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(force * Time.deltaTime);
    }
}
