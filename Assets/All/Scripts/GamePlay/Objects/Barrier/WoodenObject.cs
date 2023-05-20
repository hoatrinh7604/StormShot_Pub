using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenObject : ObjectController, Object
{
    [SerializeField] GameObject effect;
    [SerializeField] bool hasEffect;
    [SerializeField] GameObject brokenObj;

    override public void OnCollisionEnter(Collision collision)
    {
        if (IsMoving())
        {
            ExplosionObject();
            return;
        }

        if (collision.gameObject.tag == GameContracts.GROUND_TAG) return;

        if (collision.gameObject.GetComponent<Object>() != null)
        {
            if (!collision.gameObject.GetComponent<Object>().IsMoving()) return;
        }

        ExplosionObject();
    }

    override public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameContracts.DAMAGE_TAG)
            ExplosionObject();
    }

    override public bool IsMoving()
    {
        return IsObjectMoving();
    }

    public void ExplosionObject()
    {
        if (hasEffect)
        {
            var eff = Instantiate(effect, Vector3.zero, Quaternion.identity, this.transform);
            eff.transform.localPosition = Vector3.zero;
            eff.transform.SetParent(null);
            eff.transform.localScale = Vector3.one;
            eff.transform.rotation = Quaternion.Euler(0, 0, 0);
            Destroy(eff, 2);
        }
        Destroy(this.gameObject);
        BrokenObject();
    }

    public void BrokenObject()
    {
        if (hasEffect)
        {
            var eff = Instantiate(brokenObj, Vector3.zero, Quaternion.identity, this.transform);
            eff.transform.localPosition = Vector3.zero;
            eff.transform.SetParent(null);
            eff.transform.localScale = Vector3.one;
            eff.transform.rotation = Quaternion.Euler(0, 0, 0);
            //Destroy(eff, 2);
        }
        Destroy(this.gameObject);
    }

    public override void SetInfo()
    {
        ID = (int)ItemObject.WOODEN;
        type = (int)Barrel.SOFT;
    }
}
