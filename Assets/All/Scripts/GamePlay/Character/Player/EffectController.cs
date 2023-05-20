using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] GameObject eff;
    [SerializeField] Transform pos;

    GameObject effTemp;
    public void SpawnEffect()
    {
        effTemp = Instantiate(eff, Vector3.zero, Quaternion.identity, pos);
        effTemp.transform.localPosition = Vector3.zero;
        effTemp.transform.localRotation = Quaternion.Euler(-90,0,0);
    }

    public void Destroy()
    {
        Destroy(effTemp.gameObject);
    }
}
