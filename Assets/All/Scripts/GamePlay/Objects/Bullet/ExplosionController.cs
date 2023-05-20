using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GameContracts.CHARACTER_DEATH_LAYER))
        {
            collision.gameObject.GetComponentInParent<CharacterController>().BreakDeathCharacter(collision.gameObject.transform);
        }
    }
}
