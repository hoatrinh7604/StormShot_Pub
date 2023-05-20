using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameContracts.BULLET_TAG)
        {
            RemoveChildObject();
        }
    }

    private void RemoveChildObject()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
