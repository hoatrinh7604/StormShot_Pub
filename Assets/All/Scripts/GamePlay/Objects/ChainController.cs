using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainController : ObjectController
{
    [SerializeField] bool canBreak = false;
    [SerializeField] float forceScale = 5;

    override public void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameContracts.DAMAGE_TAG)
        {
            OnDamage(other);
        }
    }

    public void OnCollisionWithCharacter()
    {

    }
    public void OnCollisionWithObject()
    {
        
    }
    public void OnDamage(Collider other)
    {
        AddForce(other.ClosestPoint(transform.position));

        // On Damage of TNT
        if(other.gameObject.name == GameContracts.EXPLOSION_NAME)
        {
            Destroy(this.gameObject);
            return;
        }

        Break();
    }
    public void Break()
    {
        if (!canBreak) return;
        GetComponent<HingeController>().JointBreak();
    }

    public void AddForce(Vector3 forceTran)
    {
        if (forceTran.x > transform.position.x) forceTran.x = Mathf.Abs(forceTran.x);
        else forceTran.x = -Mathf.Abs(forceTran.x);
        rigBody.AddRelativeForce(new Vector3(forceScale*forceTran.x, 5, 0), ForceMode.Impulse);
    }

    public override void SetInfo()
    {
        ID = (int)ItemObject.CHAIN;
        type = (int)Barrel.SOFT;
    }
}
