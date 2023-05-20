using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint twoBoneIKConstraint;

    private bool canRotate = false;
    [SerializeField] Transform target;
    [SerializeField] float speedRotate = 5f;
    [SerializeField] float aimingY = -180;
    [SerializeField] float cameraY = 90;

    private void Awake()
    {
        target.SetParent(null);
        ReturnAimingFace();
    }

    private void Update()
    {
        if(canRotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, speedRotate * Time.deltaTime);
        }
    }

    public void Rotate()
    {
        twoBoneIKConstraint.weight = 0;

        var aims = GetComponentsInChildren<MultiAimConstraint>();
        foreach(var a in aims)
        {
            a.weight = 0;
        }

        canRotate = true;
    }

    public void CharacterDeath()
    {
        canRotate = false;
    }

    public void ReturnAimingFace()
    {
        canRotate = false;
        target.rotation = Quaternion.Euler(0, aimingY, 0);
    }

    public void SetLookCamera()
    {
        target.rotation = Quaternion.Euler(0, cameraY, 0);
    }
}
