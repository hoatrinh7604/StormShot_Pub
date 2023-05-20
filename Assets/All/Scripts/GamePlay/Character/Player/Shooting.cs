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
        var bulletTemp = Instantiate(bullet, shootPoint.position, Quaternion.identity, null);
        bulletTemp.transform.position = new Vector3(bulletTemp.transform.position.x, bulletTemp.transform.position.y, 0);
        bulletTemp.GetComponent<BulletMovingGeneric>().SetBaseTarget(target);
        bulletTemp.GetComponent<BulletMovingGeneric>().AddBaseForce();
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
