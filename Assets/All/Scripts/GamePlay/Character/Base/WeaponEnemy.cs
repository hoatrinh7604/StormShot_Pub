using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponEnemy : MonoBehaviour
{
    [SerializeField] GameObject itemEquip;
    [SerializeField] TwoBoneIKConstraint leftHand, rightHand;

    public void Unequip()
    {
        if (!itemEquip) return;
        var rig = itemEquip.AddComponent<Rigidbody>();
        rig.AddForce(new Vector3(0, 5, 0));
        itemEquip.transform.parent = null;
        itemEquip.transform.Rotate(new Vector3(0, 0, 90));

        if(leftHand)
        {
            leftHand.weight = 0;
        }

        if (rightHand)
        {
            rightHand.weight = 0;
        }
    }
}
