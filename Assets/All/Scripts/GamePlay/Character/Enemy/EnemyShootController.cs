using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyShootController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform crossHair;

    [SerializeField] EnemyCrosshairController enemyCrosshairController;

    [SerializeField] MultiAimConstraint body, head, Rhand;
    [SerializeField] TwoBoneIKConstraint LHand;
    [SerializeField] RigBuilder rigBuilder;
    bool haveDirectCanKillPlayer = false;
    bool canKillPlayer = false;
    bool canShot = false;

    private void Awake()
    {
        enemyCrosshairController = GameElement.Instance.enemyCrosshairController;
        crossHair = enemyCrosshairController.transform;
        rigBuilder = GetComponent<RigBuilder>();

        SetupSourceTargetBone(crossHair);
    }

    private void Update()
    {
        
    }

    private int ratioKillPlayer = 30;
    public void Shooting()
    {
        canShot = false;
        crossHair.gameObject.SetActive(false);
        characterController.SetState((int)AnimState.SHOOTING);
        var bulletTemp = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, null);
        bulletTemp.transform.position = new Vector3(bulletTemp.transform.position.x, bulletTemp.transform.position.y, 0);
        bulletTemp.GetComponent<BulletMovingGeneric>().SetBaseTarget(crossHair);
        bulletTemp.GetComponent<BulletMovingGeneric>().AddBaseForce();

        GameElement.Instance.turnBaseMode.BlockBoss(1);
    }

    public void IncreaseRatioKill(int plus)
    {
        ratioKillPlayer += plus;
    }

    bool isAiming = false;

    public void StartAiming()
    {
        if (characterController.isDeath)
        {
            return;
        }

        isAiming = true;
        UpdateAimBone();
        crossHair.gameObject.SetActive(true);
        characterController.SetAnim((int)AnimState.AIMING);
        enemyCrosshairController.MovingCrossHair(true);

        int ratioKill = Random.Range(0, 101);
        canKillPlayer = ratioKill <= ratioKillPlayer;
    }

    public void StopAiming()
    {
        enemyCrosshairController.MovingCrossHair(false);
    }

    public bool isDeath()
    {
        return characterController.isDeath;
    }

    public void HideShotLine()
    {
        crossHair.gameObject.SetActive(false);
    }

    public void ReturnIdle()
    {
        UpdateIdleBone();
        isAiming = false;
        canShot = false;
        haveDirectCanKillPlayer = false;
        crossHair.gameObject.SetActive(false);
        characterController.SetAnim((int)AnimState.IDLE);
        enemyCrosshairController.MovingCrossHair(false);
    }

    public void ShootHandle()
    {
        enemyCrosshairController.MovingCrossHair(false);
    }

    public bool CanKillPlayer()
    {
        return canKillPlayer;
    }

    public void CanShoot(bool isCanShoot)
    {
        canShot = isCanShoot;
    }

    public bool IsCanShoot()
    {
        return canShot;
    }

    public void CheckTheDirectCanKillPlayer()
    {
        haveDirectCanKillPlayer = true;
        //enemyCrosshairController.SetDirectionShoot();
    }

    public bool HaveDirectToKillPlayer()
    {
        return haveDirectCanKillPlayer;
    }

    public bool IsInDirectCanKillPlayer()
    {
        return enemyCrosshairController.InDirectCanKillPlayer();
    }

    public void ReworkBone()
    {
        if(isAiming)
        {
            UpdateAimBone();
            characterController.SetAnim((int)AnimState.AIMING);
        }
        else
        {
            UpdateIdleBone();
        }
    }

    public void UpdateIdleBone()
    {
        head.weight = 0;
        body.weight = 0;
        Rhand.weight = 0;
        LHand.weight = 0;
    }

    public void UpdateAimBone()
    {
        head.weight = 0.5f;
        body.weight = 0.5f;
        Rhand.weight = 1;
        LHand.weight = 1;
    }

    private void SetupSourceTargetBone(Transform target)
    {
        var head_ = head.data.sourceObjects;
        head_.SetTransform(0, target);
        head.data.sourceObjects = head_;

        var body_ = body.data.sourceObjects;
        body_.SetTransform(0, target);
        body.data.sourceObjects = body_;

        var RHand_ = Rhand.data.sourceObjects;
        RHand_.SetTransform(0, target);
        Rhand.data.sourceObjects = RHand_;

        rigBuilder.Build();
    }

    public Transform GetShootPoint()
    {
        return shootPoint;
    }
}
