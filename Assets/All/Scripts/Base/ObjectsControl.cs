using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsControl : MonoBehaviour
{
    Vector3 basePos;

    private void Awake()
    {
        basePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public bool IsMoving()
    {
        if ((transform.position - basePos).magnitude > 2f)
            return true;
        return false;
    }
}
