using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] CharacterController characterController;
    [SerializeField] int facingDirect = -1;
    [SerializeField] float minX, maxX;
    [SerializeField] float speed;

    [SerializeField] bool WalkOnWake = false;
    [SerializeField] float delayToWalk = 2;
    [SerializeField] float baseRotY = 215;
    private Vector3 basePos;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        basePos = new Vector3(transform.position.x, transform.position.y, 0);
        SetTarget();
        rigidBody.drag = 0.4f;
        if(WalkOnWake)
        {
            Walking();
        }
    }

    bool walking = false;
    //bool canWalk = false;
    private float timing;
    private void Update()
    {
        //timing -= Time.deltaTime;
        //if(characterController.GetState() == (int)AnimState.IDLE)
        //{
        //    canWalk = true;
        //}
        //else
        //{
        //    canWalk = false;
        //}

        //if(canWalk)
        //{
        //    if(timing < 0)
        //    {
        //        timing = delayToWalk;
        //        walking = true;
        //    }
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(walking)
        {
            Reserve();
            Move();
        }
    }


    public void Walking()
    {
        walking = true;
        characterController.SetState((int)AnimState.WALKING);
    }

    public void Idle()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        walking = false;
        characterController.SetState((int)AnimState.IDLE);
    }

    private void SetTarget()
    {
        if(facingDirect < 0)
        {
            target = new Vector3(basePos.x - minX, basePos.y, 0);
        }
        else
        {
            target = new Vector3(basePos.x + maxX, basePos.y, 0);
        }
    }

    public void Move()
    {
        rigidBody.AddForce(new Vector3(facingDirect * Time.deltaTime * speed, 0, 0));
    }

    public void Reserve()
    {
        if(facingDirect < 0)
        {
            if(transform.position.x < basePos.x - minX)
            {
                facingDirect = facingDirect * -1;
                SetTarget();
                FlipObject();
                StartCoroutine(DelayToWalk());
            }    
        }
        else
        {
            if (transform.position.x > basePos.x + maxX)
            {
                facingDirect = facingDirect * -1;
                FlipObject();
                SetTarget();
                StartCoroutine(DelayToWalk());
            }
        }
    }

    public void FlipObject()
    {
        baseRotY *= -1;
        transform.rotation = Quaternion.Euler(0, baseRotY, 0);
    }

    IEnumerator DelayToWalk()
    {
        Idle();
        walking = false;
        yield return new WaitForSeconds(delayToWalk);
        Walking();
        walking = true;
    }
}
