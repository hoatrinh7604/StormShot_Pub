using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumChildController : MonoBehaviour
{
    [SerializeField] Vector3 force;
    [SerializeField] GameObject saw;
    [SerializeField] bool canSlice = false;

    private void Start()
    {
        saw.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }

    public void DisconnectSaw()
    {
        if (!saw.GetComponent<HingeJoint>()) return;
        saw.GetComponent<HingeJoint>().connectedBody = null;
        Destroy(saw.GetComponent<HingeJoint>());
    }

    public void ControlRope()
    {
        if(canSlice)
        {
            DisconnectSaw();
        }
    }

    public bool CanSlice()
    {
        return canSlice;
    }
}
