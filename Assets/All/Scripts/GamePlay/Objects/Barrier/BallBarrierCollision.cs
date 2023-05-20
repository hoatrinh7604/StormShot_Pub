using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBarrierCollision : BarrierGeneric
{
    Rigidbody rig;
    [SerializeField] float forceScale = 5;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    override public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Explosion")
        {
            rig.useGravity = true;
        }

        if(collision.gameObject.tag == GameContracts.BULLET_TAG)
        {
            AddForce(collision.gameObject.transform);
        }
    }


    public void AddForce(Transform forceTran)
    {
        if(forceTran.position.x < transform.position.x)
        {
            rig.AddRelativeForce(new Vector3(forceScale, 0, 0), ForceMode.Impulse);
        }
        else
        {
            rig.AddRelativeForce(new Vector3(-forceScale, 0, 0), ForceMode.Impulse);
        }
    }
}
