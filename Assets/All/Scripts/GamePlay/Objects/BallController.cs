using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BallController : ObjectController
{
    [SerializeField] float forceScale = 5;

    override public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameContracts.DAMAGE_TAG)
        {
            AddForce(collision.gameObject.transform);
        }
    }


    public void AddForce(Transform forceTran)
    {
        //if (forceTran.position.x < transform.position.x)
        //{
        //    rigBody.AddRelativeForce(new Vector3(forceScale, 5, 0), ForceMode.Impulse);
        //}
        //else
        //{
        //    rigBody.AddRelativeForce(new Vector3(-forceScale, 5, 0), ForceMode.Impulse);
        //}

        float xValue = 1;
        if (forceTran.position.x < transform.position.x) xValue = 1;
        else xValue = -1;
        rigBody.AddForce(new Vector3(forceScale * xValue, 0, 0), ForceMode.Impulse);
    }

    public override void SetInfo()
    {
        ID = (int)ItemObject.IRON_BALL;
        type = (int)Barrel.HARD;
    }
}
