using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMoving : MonoBehaviour, BarrierMoving
{
    [SerializeField] Transform firstPost;
    [SerializeField] Transform lastPost;

    [SerializeField] float speed;
    private Transform target;
    private bool startMoving = false;
    //private int direct = 1;

    private void Start()
    {
        SetTarget(lastPost);
        StartMoving();
    }

    private void Update()
    {
        if(startMoving)
        {
            Moving();
        }
    }

    public void Moving()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, target.position) < 0.01f)
        {
            Reverse();
        }
    }

    public void Reverse()
    {
        if(transform.position.x >= lastPost.position.x)
        {
            target = firstPost;
        }
        else if(transform.position.x <= firstPost.position.x)
        {
            target = lastPost;
        }
    }

    public void StartMoving()
    {
        startMoving = true;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameContracts.ENEMY_TAG)
        {
            var cc = collision.gameObject.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.SetCharacterInObject(gameObject);
            }
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == GameContracts.ENEMY_TAG)
    //    {
    //        collision.gameObject.GetComponent<CharacterController>().SetCharacterInObject(gameObject);
    //    }
    //}
}
