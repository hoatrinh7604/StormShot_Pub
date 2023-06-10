using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BombObject : ObjectController
{
    [SerializeField] GameObject explosionObj;
    [SerializeField] GameObject effect;
    [SerializeField] bool hasEffect;

    [SerializeField] float timeScape;
    [SerializeField] TextMeshProUGUI txt_Time;
    private float timer;
    public override void Update()
    {
        base.Update();

        Timer();
    }

    override public void OnCollisionEnter(Collision collision)
    {
        if (IsMoving())
        {
            ExplosionObject();
            return;
        }

        if (collision.gameObject.tag == GameContracts.GROUND_TAG) return;

        if (collision.gameObject.GetComponent<Object>() != null)
        {
            if (!collision.gameObject.GetComponent<Object>().IsMoving()) return;
        }

        ExplosionObject();
    }

    override public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameContracts.DAMAGE_TAG)
            ExplosionObject();
    }

    public void ExplosionObject()
    {
        if (hasEffect)
        {
            var eff = Instantiate(effect, Vector3.zero, Quaternion.identity, this.transform);
            eff.transform.localPosition = Vector3.zero;
            eff.transform.SetParent(null);
            eff.transform.localScale = Vector3.one;
            eff.transform.rotation = Quaternion.Euler(0, 0, 0);
            Destroy(eff, 2);
        }
        SoundController.Instance.PlayAudio(SoundController.Instance.explosion, 1, false);
        Exploision();
        Destroy(this.gameObject);
    }

    public void Exploision()
    {
        var obj = Instantiate(explosionObj, Vector3.zero, Quaternion.identity, this.transform);
        obj.gameObject.name = "Explosion";
        obj.transform.localPosition = Vector3.zero;
        obj.transform.SetParent(null);

        Destroy(obj, 1f);
    }


    public void OnCollisionWithCharacter()
    {

    }
    public void OnCollisionWithObject()
    {

    }
    public void OnDamage()
    {
        Destroy(this.gameObject);
    }

    public void Timer()
    {
        if (GameplayController.Instance.gameState == (int)GameState.PAUSE) return;
        timer -= Time.deltaTime;
        txt_Time.text = timer.ToString("0");
        if (timer <= 0)
        {
            ExplosionObject();
        }
    }

    public override void SetInfo()
    {
        ID = (int)ItemObject.BOMB;
        type = (int)Barrel.SOFT;
        timer = timeScape;
    }
}
