using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketBullet : BulletMovingGeneric, Object, BulletGeneric
{
    [SerializeField] GameObject explosionEff;
    [SerializeField] GameObject explosionObject;
    [SerializeField] GameObject bulletImage;

    public override IEnumerator Start()
    {
        bulletImage.SetActive(false);
        yield return new WaitForSeconds(timeEnableCollision);
        col.enabled = true;
        bulletImage.SetActive(true);
    }

    public int GetBulletType()
    {
        return (int)BulletType.NORMAL;
    }

    public void SetTarget(Transform target, Transform shotPoint)
    {
        SetBaseTarget(target, shotPoint);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.right = rb.velocity.normalized;
    }

    override public void OnTriggerEnter(Collider other)
    {
        Explosion();
        Destroy(this.gameObject);
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

        var eff2 = Instantiate(explosionObject, Vector3.zero, Quaternion.identity, this.transform);
        eff2.transform.localPosition = Vector3.zero;
        eff2.transform.SetParent(null);
        eff2.transform.localScale = Vector3.one;
        eff2.transform.rotation = Quaternion.Euler(0, 0, 0);
        Destroy(eff2, 2);
    }

    public bool IsMoving()
    {
        return true;
    }
}
