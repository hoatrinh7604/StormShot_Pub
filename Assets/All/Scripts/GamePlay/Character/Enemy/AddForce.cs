using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] Rigidbody rig;
    [SerializeField] Vector3 force;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        AddForceToThis();
    }

    public void AddForceToThis()
    {
        rig.AddForce(force, ForceMode.Impulse);
    }
}
