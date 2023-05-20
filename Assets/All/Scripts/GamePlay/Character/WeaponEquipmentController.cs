using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponEquipmentController : MonoBehaviour
{
    [SerializeField] Transform rightHandTransform;
    public GameObject weapon;
    [SerializeField] GameObject[] listWeapons;

    public int weaponID;

    [SerializeField] MultiAimConstraint head, body, rightHand;
    [SerializeField] TwoBoneIKConstraint leftHand;
    [SerializeField] Transform leftHand_target;
    [SerializeField] Transform leftHand_hint;

    [SerializeField] RigBuilder rigBuilder;
    [SerializeField] Transform crossHair;

    private WeaponConfigStructClass weaponConfigStructClass;
    private CharacterController characterController;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        crossHair = GameElement.Instance.crosshairMovement.transform;

        rigBuilder = GetComponent<RigBuilder>();

        SetupSourceTargetBone(crossHair);
    }

    public void EquipmentWeapon(int id)
    {
        weaponID = id;

        foreach(var weapon in listWeapons)
        {
            if(weapon.GetComponent<Weapon>().ID == id)
            {
                weaponConfigStructClass = weapon.GetComponent<Weapon>().EquipWeapon();
                DisableAnotherWeapons(id);
                CreateWeapon(weapon);
            }
        }

        if (characterController)
        {
            characterController.SetWeapon(weaponConfigStructClass);
        }
        else
        {
            GetComponent<CharacterController>().SetWeapon(weaponConfigStructClass);
        }
        leftHand_target.localPosition = weaponConfigStructClass.twoBonePosition;
        leftHand_hint.localPosition = weaponConfigStructClass.twoBonePosition;
        leftHand_target.localRotation = Quaternion.Euler(weaponConfigStructClass.twoBoneRotation.x, weaponConfigStructClass.twoBoneRotation.y, weaponConfigStructClass.twoBoneRotation.z);
        leftHand_hint.localRotation = Quaternion.Euler(weaponConfigStructClass.twoBoneRotation.x, weaponConfigStructClass.twoBoneRotation.y, weaponConfigStructClass.twoBoneRotation.z);

        SetAimBone();
    }

    public void UnEquipmentWeapon()
    {
        SetNormalBone();
    }

    public void CreateWeapon(GameObject weaponObject)
    {
        weapon = Instantiate(weaponObject, Vector3.zero, Quaternion.identity, rightHandTransform);
        weapon.transform.localPosition = weaponConfigStructClass.localPosition;
        weapon.transform.localRotation = Quaternion.Euler(weaponConfigStructClass.localRotation.x, weaponConfigStructClass.localRotation.y, weaponConfigStructClass.localRotation.z);

        GetComponent<Shooting>().SetShootPoint(weapon.GetComponent<Weapon>().GetShootPoint());
    }

    public void DisableAnotherWeapons(int current)
    {
        var listObjects = rightHandTransform.GetComponentsInChildren<Weapon>();
        foreach(var weapon in listObjects)
        {
            weapon.gameObject.SetActive(weapon.ID == current);
        }
    }

    public void SetNormalBone()
    {
        head.weight = 0;
        body.weight = 0;
        rightHand.weight = 0;
        leftHand.weight = 0;
    }

    public void SetAimBone()
    {
        head.weight = 1;
        body.weight = 0.5f;
        rightHand.weight = 1;
        leftHand.weight = 1;
    }

    private void SetupSourceTargetBone(Transform target)
    {
        var head_ = head.data.sourceObjects;
        head_.SetTransform(0, target);
        head.data.sourceObjects = head_;

        var body_ = body.data.sourceObjects;
        body_.SetTransform(0, target);
        body.data.sourceObjects = body_;

        var RHand_ = rightHand.data.sourceObjects;
        RHand_.SetTransform(0, target);
        rightHand.data.sourceObjects = RHand_;

        rigBuilder.Build();
    }

    public Transform GetShootPoint()
    {
        return weapon.GetComponent<Weapon>().GetShootPoint();
    }

    public void UpdateShootPoint()
    {
        GetComponent<Shooting>().SetShootPoint(weapon.GetComponent<Weapon>().GetShootPoint()); 
    }

    public GameObject GetBullet()
    {
        return weapon.GetComponent<Weapon>().bulletObject;
    }
}
