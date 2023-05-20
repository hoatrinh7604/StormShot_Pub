using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Start()
    {
        target = GameElement.Instance.crosshairMovement.transform;
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
