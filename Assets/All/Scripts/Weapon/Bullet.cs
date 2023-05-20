using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] Transform target;

    public bool canMove { get; private set; }

    private void Update()
    {
        Moving();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    virtual public void Moving()
    {

    }

    public void StartMoving()
    {
        canMove = true;
    }

    public void DestroyThisObject()
    {
        //
        Destroy(this.gameObject);
    }
}
