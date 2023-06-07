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
        itemEquip.transform.parent = null;
        var rig = itemEquip.AddComponent<Rigidbody>();
        rig.AddTorque(new Vector3(0, 0, 100));
        //rig.AddForce(new Vector3(0, 10, 0));
        //itemEquip.transform.Rotate(new Vector3(0, 0, 90));
    }
}
