using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    NORMAL = 0,
    STRENGTHEN
}

public interface BulletGeneric
{
    public int GetBulletType();
    public void SetTarget(Transform target, Transform shotPoint);
    public void AddForce();

    public void Explosion();
}
