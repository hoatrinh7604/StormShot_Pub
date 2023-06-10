using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody rig;
    protected Collider col;
    [SerializeField] protected EnemySliderController healthBar;
    [SerializeField] protected RagdollDeath ragdollDeath;
    [SerializeField] protected float hp;
    public bool isDeath = false;
    [SerializeField] protected int state;
    [SerializeField] protected bool showSlider = true;

    [SerializeField] GameObject bloodEffect;

    [SerializeField] GameObject bloodEffectLoop;
    [SerializeField] Transform bloodSpaw;

    [SerializeField] GameObject breakCharacter;
    [SerializeField] GameObject brokenCharacter;
    // Start is called before the first frame update

    [SerializeField] bool voiceOnWake = false;
    [SerializeField] float delaySpeak = 3f;
    private float timing;

    private string aimAnimationName;
    private string shootAnimationName;
    private GameObject m_bloodEffect;

    virtual public void Awake()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        ragdollDeath = GetComponent<RagdollDeath>();
        timing = Random.Range(0.5f, 1);
        SetState(state);
        SetInfo(hp);
    }

    private void Update()
    {
        if(voiceOnWake)
        {
            timing-= Time.deltaTime;
            if(timing < 0 && !isDeath && !GameplayController.Instance.IsPauseGame())
            {
                PlayWakeSound();
                timing = delaySpeak;
            }
        }
    }

    virtual public void PlayWakeSound()
    {

    }

    public void SetAnim(int state)
    {
        ResetState();
        switch(state)
        {
            case (int)AnimState.IDLE: 
                anim.Play("Idle");
                break;
            case (int)AnimState.AIMING:
                anim.Play(aimAnimationName);
                break;
            case (int)AnimState.SHOOTING:
                anim.Play(shootAnimationName);
                break;
            case (int)AnimState.HIT:
                anim.Play("Hit");
                break;
            case (int)AnimState.VICTORY:
                anim.Play("Victory");
                break;
            case (int)AnimState.DEFEAT:
                anim.Play("Defeat");
                break;
            case (int)AnimState.WALKING:
                anim.Play("Walking");
                break;
            default: break;
        }
        this.state = state;
        anim.SetInteger("State", state);
    }

    private void ResetState()
    {
        anim.SetInteger("State", 0);
    }

    public int GetState()
    {
        return state;
    }

    public void SetInfo(float HP)
    {
        hp = HP;
        healthBar.SetSlider(HP);
        healthBar.transform.parent.gameObject.SetActive(showSlider);
    }

    #region Physic control
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == GameContracts.EXPLOSION_NAME)
        if (other.gameObject.GetComponent<ExplosionController>())
        {
            BreakCharacter(null);
        }
        else if(other.gameObject.GetComponent<BulletGeneric>() != null)
        {
            if (other.gameObject.GetComponent<RocketBullet>() != null) return;
            if (other.gameObject.GetComponent<SniperBullet>() != null) return;
            BloodEffect();
            Death(other.gameObject.transform.position);
        }
        else if (other.gameObject.GetComponent<DamageConfig>() != null)
        {
            if (other.gameObject.GetComponent<RocketBullet>() != null) return;
            if (other.gameObject.GetComponent<SniperBullet>() != null) return;
            BloodEffect();
            Death(other.gameObject.transform.position);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameContracts.DAMAGE_TAG)
        {
            OnColliderWithBullet(collision);
            return;
        }

        if (collision.gameObject.tag == GameContracts.TRAP_TAG || collision.gameObject.GetComponent<SawController>() || collision.gameObject.GetComponent<BallController>())
        {
            OnColliderWithBarrier(collision);
            return;
        }

        if (collision.gameObject.tag == GameContracts.GROUND_TAG) return;

        if (collision.gameObject.GetComponent<Object>() != null)
        {
            if (!collision.gameObject.GetComponent<Object>().IsMoving()) return;
        }

        OnColliderWithBarrier(collision);
    }

    virtual public void OnColliderWithBullet(Collision collision)
    {
        if (isDeath) return;
        //if (collision.gameObject.name == GameContracts.EXPLOSION_NAME)
        if (collision.gameObject.GetComponent<ExplosionController>())
        {
            BreakCharacter(collision);
        }
        else
        {
            OnColliderHandle(collision);
        }
    }

    virtual public void OnColliderWithBarrier(Collision collision)
    {
        if (isDeath) return;

        // Check pos
        if (collision.gameObject.GetComponent<SawController>() || collision.gameObject.GetComponent<BallController>())
        {
            //StartCoroutine(DisableAfterTime(0));
            //BrokenCharacter(collision);
            OnColliderHandle(collision);
        }
        else
        {
            if (transform.position.y >= collision.transform.position.y) return;
            //if (collision.gameObject.name == GameContracts.TNT_NAME) return;
            if (collision.gameObject.GetComponent<PoisionObject>()) return;
            OnColliderHandle(collision);
        }
    }

    public void OnColliderHandle(Collision collision)
    {
        if (!collision.gameObject.GetComponent<DamageConfig>()) return;
        int getDamage = collision.gameObject.GetComponent<DamageConfig>().GetDamage();

        hp -= getDamage;
        SetAnim((int)AnimState.HIT);
        //InitBloodEffect(collision.gameObject.transform);
        if (healthBar.transform.parent.gameObject.activeSelf)
            healthBar.UpdateSlider(hp);
        if (hp <= 0)
        {
            BloodEffect();
            Death(collision);
        }
    }
    #endregion

    #region Effect Control

    public void BreakCharacter(Collision collision)
    {
        StartCoroutine(DisableAfterTime());
        if (isDeath)
        {
            gameObject.SetActive(false);

            var newCharacter = Instantiate(breakCharacter, bloodSpaw.position, Quaternion.identity, bloodSpaw);
            newCharacter.transform.localPosition = Vector3.zero;
            newCharacter.transform.parent = null;

            col.enabled = false;
            return;
        }
        gameObject.SetActive(false);

        var character = Instantiate(breakCharacter, Vector3.zero, Quaternion.identity, transform);
        character.transform.localPosition = Vector3.zero;
        character.transform.parent = null;

        Death(collision);
    }

    public void BreakDeathCharacter(Transform tran)
    {
        if (tran == null) tran = transform;
        //if (isDeath) return;
        gameObject.SetActive(false);

        var character = Instantiate(breakCharacter, Vector3.zero, Quaternion.identity, tran);
        character.transform.localPosition = Vector3.zero;
        character.transform.parent = null;

        //Death(collision);
    }

    public void BrokenCharacter(Collision collision)
    {
        var replacement = Instantiate(brokenCharacter, Vector3.zero, Quaternion.identity, transform);
        replacement.transform.localPosition = Vector3.zero;
        replacement.transform.parent = null;

        Death(collision);
    }

    public void InitBloodEffect(Transform point)
    {
        var eff = Instantiate(bloodEffect, Vector3.zero, Quaternion.identity, point);
        eff.transform.localPosition = Vector3.zero;
        eff.transform.localScale = Vector3.one;
        eff.transform.SetParent(null);
        Destroy(eff, 0.3f);
    }

    public void BloodEffect()
    {
        if (m_bloodEffect) return;
        m_bloodEffect = Instantiate(bloodEffectLoop, bloodSpaw.position, Quaternion.identity, bloodSpaw);
        m_bloodEffect.transform.localRotation = Quaternion.Euler(-m_bloodEffect.transform.localRotation.x, m_bloodEffect.transform.localRotation.y, m_bloodEffect.transform.localRotation.z);
    }
    #endregion

    #region Weapon Control
    public void SetWeapon(WeaponConfigStructClass weaponConfigStructClass)
    {
        aimAnimationName = weaponConfigStructClass.aimingAnimation;
        shootAnimationName = weaponConfigStructClass.shootingAnimation;
    }
    #endregion

    public void SetCharacterInObject(GameObject obj)
    {
        transform.SetParent(obj.transform);
        //rig.isKinematic = true;
    }

    virtual public void SetState(int state)
    {
        SetAnim(state);
    }

    virtual public void Death(Collision collision)
    {
        if (isDeath) return;
        healthBar.gameObject.SetActive(false);
        isDeath = true;
        ragdollDeath.Death(collision);
        gameObject.transform.SetParent(null);
        rig.isKinematic = true;
        col.isTrigger = true;

        if(GetComponent<ItemUnequip>())
        {
            GetComponent<ItemUnequip>().Unequip();
        }
    }

    virtual public void Death(Vector3 position)
    {
        if (isDeath) return;
        healthBar.gameObject.SetActive(false);
        isDeath = true;
        ragdollDeath.Death(position);
        gameObject.transform.SetParent(null);
        rig.isKinematic = true;
        col.isTrigger = true;

        if (GetComponent<ItemUnequip>())
        {
            GetComponent<ItemUnequip>().Unequip();
        }
    }

    public IEnumerator DisableAfterTime(float time = 0.1f)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
