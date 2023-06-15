using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainController : ObjectController
{
    [SerializeField] bool canBreak = false;
    [SerializeField] float forceScale = 5;
    [SerializeField] GameObject igroneObject;

    override public void OnTriggerEnter(Collider other)
    {
        print("trigger Ondamage: " + other.gameObject.name);
        if(other.tag == GameContracts.DAMAGE_TAG)
        {
            OnDamage(other);
        }
        else
        {
            var saw = other.gameObject.GetComponent<SawController>();
            if(saw != null && igroneObject != saw.gameObject)
            {
                OnDamage(other);
                return;
            }

            var ball = other.gameObject.GetComponent<BallController>();
            if (ball != null && igroneObject != ball.gameObject)
            {
                OnDamage(other);
                return;
            }
        }
    }

    override public void OnCollisionEnter(Collision collision)
    {
        print("collision Ondamage: " + collision.gameObject.name);
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
        //if(other.gameObject.name == GameContracts.EXPLOSION_NAME)
        if(other.gameObject.GetComponent<ExplosionController>())
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
