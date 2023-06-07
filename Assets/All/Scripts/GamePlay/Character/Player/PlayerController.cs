using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController : CharacterController
{
    [SerializeField] Shooting shooting;
    [SerializeField] GameObject bulletObject;
    [SerializeField] Transform crossHair;
    Vector3 rotation;
    [SerializeField] float speedRotation = 5;

    [SerializeField] CharacterRotation characterRotation;

    bool facingRight = true;
    int amount = 90;
    bool canFlip = true;

    public bool canShoot = true;
    [SerializeField] bool hasShield = false;

    private WeaponEquipmentController weaponEquipmentController;

    override public void Awake()
    {
        characterRotation = GetComponent<CharacterRotation>();
        crossHair = GameElement.Instance.crosshairMovement.transform;
        weaponEquipmentController = GetComponent<WeaponEquipmentController>();

        CheckPosition();
        shieldObj.SetActive(false);

        //EquipWeapon(2);
        base.Awake();
    }

    private void Update()
    {
        CheckPosition();
    }

    public void CheckPosition()
    {
        if (!canFlip) return;
        if(crossHair.transform.position.x > transform.position.x)
        {
            if(!facingRight)
            {
                rotation = Vector3.up;
                Flip(); 
            }
        }
        else
        {
            if (facingRight)
            {
                rotation = Vector3.down;
                Flip();
            }
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        amount = amount * -1;
        transform.rotation = Quaternion.Euler(transform.rotation.x, amount, transform.rotation.z);
    }

    public void Rotation()
    {
        transform.Rotate(rotation * speedRotation * Time.deltaTime);
    }

    public void Shooting(int type, Transform crossHair)
    {
        SetAnim((int)AnimState.SHOOTING);
        shooting.ShootingBullet(bulletObject, crossHair);

        if (SoundController.Instance != null)
        {
            SoundController.Instance.PlayAudio(SoundController.Instance.firing, PlayerPrefs.GetFloat("EffectSound", 1), false);
        }

        StartCoroutine(ReturnAiming());
    }

    override public void SetState(int state)
    {
        if (state == (int)AnimState.AIMING || state == (int)AnimState.SHOOTING)
        {
            characterRotation.ReturnAimingFace();
            canFlip = true;
            weaponEquipmentController.SetAimBone();
        }
        else
        {
            characterRotation.SetLookCamera();
            weaponEquipmentController.SetNormalBone();
        }

        SetAnim(state);
    }

    public void Idle()
    {
        weaponEquipmentController.SetNormalBone();
        SetAnim((int)AnimState.IDLE);
        canShoot = false;
    }

    public void PlayerTurn()
    {
        weaponEquipmentController.SetAimBone();
        SetAnim((int)AnimState.AIMING);
        canShoot = true;
    }

    public void WinningState()
    {
        canFlip = false;
        characterRotation.Rotate();
    }

    public void DefeatState()
    {
        canFlip = false;
        if(!isDeath)
            characterRotation.Rotate();
    }

    IEnumerator ReturnAiming()
    {
        yield return new WaitForSeconds(0.3f);
        SetAnim((int)AnimState.AIMING);
    }

    override public void OnColliderWithBarrier(Collision collision)
    {
        if (isDeath) return;

        // Check pos
        if (collision.gameObject.name == GameContracts.SAW_NAME)
        {
            StartCoroutine(DisableAfterTime(0));
            BrokenCharacter(collision);
        }
        else
        {
            if (transform.position.y >= collision.transform.position.y) return;
            if (collision.gameObject.GetComponent<PoisionObject>()) return;
            OnColliderHandle(collision);
        }
    }

    [SerializeField] GameObject shieldObj;
    override public void Death(Collision collision)
    {
        if (isDeath) return;
        base.Death(collision);
        col.enabled = false;
        canFlip = false;
        GameplayController.Instance.PlayerDeath();

        if (SoundController.Instance == null) return;
        SoundController.Instance.PlayAudio(SoundController.Instance.playerDeath, PlayerPrefs.GetFloat("EffectSound", 1), false);
    }

    override public void Death(Vector3 position)
    {
        if (isDeath) return;
        base.Death(position);
        col.enabled = false;
        canFlip = false;
        GameplayController.Instance.PlayerDeath();

        if (SoundController.Instance == null) return;
        SoundController.Instance.PlayAudio(SoundController.Instance.playerDeath, PlayerPrefs.GetFloat("EffectSound", 1), false);
    }

    override public void OnColliderWithBullet(Collision collision)
    {
        if (isDeath) return;
        if (hasShield)
        {
            shieldObj.SetActive(false);
            GetComponent<EffectController>().Destroy();
            hasShield = false;
        }
        else
        {
            if (collision.gameObject.GetComponent<ExplosionController>())
            {
                StartCoroutine(DisableAfterTime());
                BreakCharacter(collision);
            }
            else
            {
                OnColliderHandle(collision);
            }
        }
    }

    public void EquipWeapon(int ID)
    {
        weaponEquipmentController.EquipmentWeapon(ID);
        bulletObject = weaponEquipmentController.GetBullet();
    }

    override public void PlayWakeSound()
    {
        //SoundController.Instance.PlayAudio(SoundController.Instance.plSmile, PlayerPrefs.GetFloat("EffectSound", 1), false);
    }

    public void ShieldControl(bool isEnable)
    {
        if(isEnable)
        {
            shieldObj.SetActive(true);
            hasShield = true;
            GetComponent<EffectController>().SpawnEffect();
        }
    }

}
