using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeController : MonoBehaviour
{
    [SerializeField] HingeJoint connected;

    public void JointBreak()
    {
        Destroy(connected);
    }

    private void OnDestroy()
    {
        Destroy(connected);
    }
}
