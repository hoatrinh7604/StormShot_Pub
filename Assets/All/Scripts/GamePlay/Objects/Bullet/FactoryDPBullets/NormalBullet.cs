using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BulletMovingGeneric, Object, BulletGeneric
{
    [SerializeField] GameObject explosionEff;

    public int GetBulletType()
    {
        return (int)BulletType.NORMAL;
    }

    public void SetTarget(Transform target)
    {
        SetBaseTarget(target);
    }

    public void AddForce()
    {
        AddBaseForce();
    }

    public void Explosion()
    {
        var eff = Instantiate(explosionEff, Vector3.zero, Quaternion.identity, this.transform);
        eff.transform.localPosition = Vector3.zero;
        eff.transform.SetParent(null);
        eff.transform.localScale = Vector3.one;
        eff.transform.rotation = Quaternion.Euler(0, 0, 0);
        Destroy(eff, 2);
    }

    public bool IsMoving()
    {
        return true;
    }
}
