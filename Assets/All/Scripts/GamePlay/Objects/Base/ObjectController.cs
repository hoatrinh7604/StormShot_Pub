using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectController : MonoBehaviour, Object
{
    public Rigidbody rigBody;
    public float velocityMoving = 5;

    public int ID { get; protected set; }
    public int type { get; protected set; }
    public float velocity { get; private set; }

    private float time;
    // Start is called before the first frame update
    void Awake()
    {
        rigBody = GetComponent<Rigidbody>();
        SetInfo();
    }

    // Update is called once per frame
    virtual public void Update()
    {
        UpdateSpeedEachTime();
    }

    public void UpdateSpeedEachTime()
    {
        time += Time.deltaTime;
        if(time >= 0.5f)
        {
            velocity = rigBody.velocity.magnitude;
            time = 0;
        }
    }

    public bool IsObjectMoving()
    {
        return velocity > velocityMoving;
    }

    virtual public bool IsMoving()
    {
        return velocity > velocityMoving;
    }

    virtual public void OnDestroyObject()
    {

    }

    virtual public void OnTriggerEnter(Collider other)
    {
        
    }

    virtual public void OnCollisionEnter(Collision collision)
    {
        
    }

    virtual public void SetInfo()
    {

    }
}
