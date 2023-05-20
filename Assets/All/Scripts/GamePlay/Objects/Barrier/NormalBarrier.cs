using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBarrier : BarrierGeneric
{
    [SerializeField] GameObject effect;
    [SerializeField] bool hasEffect;

    override public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != GameContracts.GROUND_TAG)
        {
            ExplosionObject();
        }
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
    }

}
