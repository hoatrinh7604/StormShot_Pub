using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeObjectControl : MonoBehaviour
{
    [SerializeField] HingeJoint connect;

    private void OnDestroy()
    {
        Destroy(connect);
    }
}
