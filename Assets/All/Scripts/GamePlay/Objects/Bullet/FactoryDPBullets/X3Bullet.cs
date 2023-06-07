using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X3Bullet : BulletMovingGeneric, Object, BulletGeneric
{
    [SerializeField] GameObject childBullet;
    [SerializeField] GameObject explosionEff;
    [SerializeField] float timeToExploision;

    private bool isExplosed = false;

    override public void FixedUpdate()
    {
        base.FixedUpdate();
        if (time > 1.0f && !isExplosed)
        {
            SpawX2();
            isExplosed = true;
            Explosion();
        }
    }

    private void SpawX2()
    {
        Vector2 directionUp = Quaternion.Euler(0, 0, -30) * direction;
        var newBulletUp = Instantiate(childBullet, transform.position, transform.rotation);
        newBulletUp.GetComponent<BulletMovingGeneric>().direction = directionUp;
        newBulletUp.GetComponent<BulletMovingGeneric>().AddNewForce();

        Vector2 directionDown = Quaternion.Euler(0, 0, 30) * direction;
        var newBulletDown = Instantiate(childBullet, transform.position, transform.rotation);
        newBulletDown.GetComponent<BulletMovingGeneric>().direction = directionDown;
        newBulletDown.GetComponent<BulletMovingGeneric>().AddNewForce();
    }

    public int GetBulletType()
    {
        return (int)BulletType.NORMAL;
    }

    public void SetTarget(Transform target, Transform shotPoint)
    {
        SetBaseTarget(target, shotPoint);
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
