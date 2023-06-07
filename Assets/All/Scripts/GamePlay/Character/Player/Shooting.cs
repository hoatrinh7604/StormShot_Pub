using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] LineShotController lineShotController;

    private void Awake()
    {
        lineShotController = FindObjectOfType<LineShotController>();
    }

    public void ShootingBullet(GameObject bullet, Transform target)
    {
        if(bullet.GetComponent<SniperBullet>() != null)
        {
            ShootingSniper(bullet, target);
            return;
        }

        var bulletTemp = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        //bulletTemp.transform.localPosition = new Vector3(bulletTemp.transform.localPosition.x, bulletTemp.transform.localPosition.y, 0);
        bulletTemp.GetComponent<BulletMovingGeneric>().SetBaseTarget(target, shootPoint);
        bulletTemp.GetComponent<BulletMovingGeneric>().AddBaseForce();
    }

    public void ShootingSniper(GameObject bullet, Transform target)
    {
        var bulletTemp = Instantiate(bullet, target.position, target.rotation);
    }

    public Transform GetShootPoint()
    {
        return GetComponent<WeaponEquipmentController>().GetShootPoint();
    }

    public void SetShootPoint(Transform target)
    {
        shootPoint = target;

        if(lineShotController == null)
        {
            lineShotController = FindObjectOfType<LineShotController>();
        }
        lineShotController.SetShootPoint(shootPoint);
    }
}
