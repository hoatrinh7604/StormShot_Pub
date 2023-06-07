using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletObject;
    [SerializeField] Transform shotPointTransform;

    public Transform targetTransform { get; protected set; }
    public int ID;
    public int bullet;

    private void Awake()
    {
        
    }

    virtual public void Shoot()
    {
        var newBullet = Instantiate(bulletObject, Vector3.zero, Quaternion.identity, shotPointTransform);
    }

    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }

    public Transform GetShootPoint()
    {
        return shotPointTransform;
    }

    virtual public WeaponConfigStructClass EquipWeapon()
    {
        return new WeaponConfigStructClass();
    }

    virtual public void UnEquipWeapon()
    {

    }

    virtual public void SetInfo(Transform target, int moreBullet = 0)
    {

    }
}
