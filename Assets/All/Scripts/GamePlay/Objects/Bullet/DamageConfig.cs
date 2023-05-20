using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageConfig : MonoBehaviour
{
    [SerializeField] int damage = 1;

    public int GetDamage()
    {
        return damage;
    }
}
