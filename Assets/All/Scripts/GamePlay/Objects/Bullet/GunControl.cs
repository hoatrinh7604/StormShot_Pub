using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField] Rigidbody rig;

    private void Start()
    {
        rig.centerOfMass = Vector3.zero;
    }
}
