using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovingGeneric : MonoBehaviour
{
    protected Rigidbody rb;
    protected Collider col;
    protected Vector3 LastVelocity;

    [SerializeField] float speed;
    [SerializeField] Transform crosshair;
    [SerializeField] int numberOfCol = 0;
    [SerializeField] float timeEnableCollision = 0.1f;

    private bool canEnableCollision = true;
    private Vector3 target;
    [SerializeField] float time = 6;
    // Start is called before the first frame update
    void Awake()
    {
        SetPreValue();
        //Destroy(this.gameObject, 5f);
        col.enabled = false;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeEnableCollision);
        col.enabled = true;
    }

    public void SetPreValue()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        //col.isTrigger = true;
        //StartCoroutine(EnableCollision());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            ExplosionBullet();
        }

        LastVelocity = rb.velocity;
    }

    public void SetBaseTarget(Transform target)
    {
        crosshair = target;
        this.target = new Vector3(target.position.x, target.position.y, target.position.z);
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.01f*Time.deltaTime);
        col.isTrigger = false;
    }

    public void AddBaseForce()
    {
        if(rb == null)
        {
            SetPreValue();
        }

        Vector2 direction = new Vector2(crosshair.position.x - this.transform.position.x, crosshair.position.y - this.transform.position.y);
        direction = direction.normalized;
        rb.AddForce(new Vector2(direction.x * speed, direction.y * speed));
    }

    #region Collision
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == GameContracts.CYLINDER_TAG)
        //{
        //    OnCollisionWithCylinder(other.gameObject);
        //}
    }

    private void OnCollisionWithCylinder(GameObject cylinder)
    {
        var parent = cylinder.GetComponentInParent<PendulumChildController>();
        if(parent.CanSlice())
        {
            var obj = cylinder.GetComponent<HingeJoint>();

            if (obj)
            {
                Destroy(obj);
            }

            cylinder.GetComponentInParent<PendulumChildController>().ControlRope();
        }
    }

    private void OnCollisionWithNormalBarrier()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionWithHardBarrier(Collision col)
    {
        SoundController.Instance.PlayAudio(SoundController.Instance.bang, PlayerPrefs.GetFloat("EffectSound", 1), false);
        ReflectDirect(col.contacts[0].normal);
        numberOfCol++;
        CheckDestroy();
    }

    private void OnCollisionWithOutSide()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionWithCharacter(int characterType)
    {
        Destroy(this.gameObject);
        if (!canEnableCollision) return;
        canEnableCollision = false;
        StartCoroutine(FixedCollision());

        switch (characterType)
        {
            case (int)CHARACTER.PLAYER:
                //collision.gameObject.GetComponentInParent<PlayerController>().OnColliderWithBullet();
                break;
            case (int)CHARACTER.ENEMY:
                //collision.gameObject.GetComponentInParent<EnemyController>().OnColliderWithBullet();
                break;
            case (int)CHARACTER.HOSTAGE:
                //collision.gameObject.GetComponentInParent<HostageController>().OnColliderWithBullet();
                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameContracts.NORMAL_BARRIER_TAG)
        {
            OnCollisionWithNormalBarrier();
        }
        else if(collision.gameObject.tag == GameContracts.HARD_BARRIER_TAG)
        {
            OnCollisionWithHardBarrier(collision);
        }
        else if (collision.gameObject.tag == GameContracts.ENEMY_TAG)
        {
            OnCollisionWithCharacter((int)CHARACTER.ENEMY);
        }
        else if (collision.gameObject.tag == GameContracts.HOSTAGE_TAG)
        {
            OnCollisionWithCharacter((int)CHARACTER.HOSTAGE);
        }
        else if (collision.gameObject.tag == GameContracts.GROUND_TAG)
        {
            OnCollisionWithHardBarrier(collision);
        }
        else if (collision.gameObject.tag == GameContracts.PLAYER_TAG)
        {
            OnCollisionWithCharacter((int)CHARACTER.PLAYER);
        }
        else if (collision.gameObject.tag == GameContracts.OUTSIDE_TAG)
        {
            OnCollisionWithOutSide();
        }
    }
    #endregion

    #region Bullet moving behavior
    IEnumerator FixedCollision()
    {
        yield return new WaitForSeconds(0.1f);
        canEnableCollision = true;
    }

    private void ReflectDirect(Vector3 normal)
    {
        var speed = LastVelocity.magnitude;
        var direction = Vector3.Reflect(LastVelocity.normalized, normal);
        direction = new Vector3(direction.x, direction.y, 0);
        rb.velocity = direction * Mathf.Max(speed, 0f);
    }
    #endregion

    private void CheckDestroy()
    {
        if(numberOfCol >= 10)
        {
            ExplosionBullet();
        }
    }

    public void ExplosionBullet()
    {
        if(SoundController.Instance)
            SoundController.Instance.PlayAudio(SoundController.Instance.bang, PlayerPrefs.GetFloat("EffectSound", 1), false);

        GetComponent<BulletGeneric>().Explosion();
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (GameplayController.Instance)
        {
            GameplayController.Instance.BulletOnDestroy();
        }
    }
}
