using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEffect : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] float reScale = 0.01f;
    private void Start()
    {
        Effect(transform);
    }

    public void Effect(Transform parent)
    {
        if (effect == null) return;
        var eff = Instantiate(effect, Vector3.zero, Quaternion.identity, parent);
        eff.transform.localPosition = Vector3.zero;
        eff.transform.localScale = new Vector3(reScale, reScale, reScale);
        eff.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
