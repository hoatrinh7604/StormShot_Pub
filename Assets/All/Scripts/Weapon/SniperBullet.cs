using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : BulletMovingGeneric, Object, BulletGeneric
{
    public GameObject effectPrefab;
    public float timeToInteraction = 2.0f;

    public override void FixedUpdate()
    {
        //
    }

    public override void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject, 1.0f);
    }

    [SerializeField] GameObject explosionEff;

    public int GetBulletType()
    {
        return (int)BulletType.NORMAL;
    }

    public void SetTarget(Transform target, Transform shotPoint)
    {
        SetBaseTarget(target, shotPoint);
    }

    public override void AddBaseForce()
    {
        StartCoroutine(SpawBullet());
    }

    IEnumerator SpawBullet()
    {
        GameObject effect = Instantiate(effectPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(timeToInteraction);
        col.enabled = true;
        Destroy(this.gameObject, 3.0f);
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
