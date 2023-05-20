using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnequip : MonoBehaviour
{
    [SerializeField] GameObject itemEquip;

    private WeaponEquipmentController weaponEquipmentController;

    private void Awake()
    {
        weaponEquipmentController = GetComponent<WeaponEquipmentController>();
    }

    public void Unequip()
    {
        itemEquip = weaponEquipmentController.weapon.gameObject;
        var rig = itemEquip.AddComponent<Rigidbody>();
        rig.AddForce(new Vector3(0, 5, 0));
        itemEquip.transform.parent = null;
        itemEquip.transform.Rotate(new Vector3(0, 0, 90));
    }
}
