using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisionObject : ObjectController
{
    [SerializeField] GameObject explosionObj;
    [SerializeField] GameObject effect;
    [SerializeField] bool hasEffect;

    private bool isExplosion = false;

    override public void OnCollisionEnter(Collision collision)
    {
        if (isExplosion) return;
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

    public void ExplosionObject()
    {
        if (hasEffect)
        {
            var eff = Instantiate(effect, transform.position, transform.rotation);
            Destroy(eff, 3);
        }
        SoundController.Instance.PlayAudio(SoundController.Instance.explosion, 1, false);
        Exploision();
        isExplosion = true;
        Destroy(this.gameObject);
    }

    public void Exploision()
    {
        var obj = Instantiate(explosionObj, Vector3.zero, Quaternion.identity, this.transform);
        obj.gameObject.name = "Explosion";
        obj.transform.localPosition = Vector3.zero;
        obj.transform.SetParent(null);

        Destroy(obj, 1f);
    }


    public void OnCollisionWithCharacter()
    {

    }
    public void OnCollisionWithObject()
    {

    }
    public void OnDamage()
    {
        Destroy(this.gameObject);
    }
    public void DestroyThis()
    {

    }

    public override void SetInfo()
    {
        ID = (int)ItemObject.POISON_BARREL;
        type = (int)Barrel.SOFT;
    }
}
