using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGunWeapon : Weapon
{
    [SerializeField] string aimingAnimation;
    [SerializeField] string shootingAnimation;
    [SerializeField] Vector3 localPosition;
    [SerializeField] Vector3 localRotation;
    [SerializeField] Vector3 twoBonePosition;
    [SerializeField] Vector3 twoBoneRotation;

    private WeaponConfigStructClass weaponConfigStructClass;
    override public WeaponConfigStructClass EquipWeapon()
    {
        weaponConfigStructClass = new WeaponConfigStructClass();
        weaponConfigStructClass.ID = ID;
        weaponConfigStructClass.aimingAnimation = aimingAnimation;
        weaponConfigStructClass.shootingAnimation = shootingAnimation;
        weaponConfigStructClass.localPosition = localPosition;
        weaponConfigStructClass.localRotation = localRotation;
        weaponConfigStructClass.twoBonePosition = twoBonePosition;
        weaponConfigStructClass.twoBoneRotation = twoBoneRotation;

        return weaponConfigStructClass;
    }

    override public void UnEquipWeapon()
    {

    }

    public override void SetInfo(Transform target, int moreBullet = 0)
    {
        ID = (int)Weapons.LONG_GUN;
        weaponConfigStructClass.ID = ID;
        bullet += moreBullet;
        targetTransform = target;
    }
}
