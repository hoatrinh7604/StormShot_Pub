using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float limit;
    [SerializeField] bool randomStart;
    [SerializeField] float random;

    [SerializeField] bool canMove;
    [SerializeField] GameObject childObject;
    [SerializeField] Transform parent;
    private void Start()
    {
        if(randomStart)
        {
            random = Random.Range(0, 1f);
        }
    }

    private void Update()
    {
        if (canMove)
        {
            float angle = limit * Mathf.Sin(Time.time + random * speed);
            parent.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameContracts.BULLET_TAG)
        {
            StartCoroutine(RemoveChildObject(other.gameObject.transform.position.normalized * 5));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameContracts.BULLET_TAG)
        {
            //Destroy(GetComponent<HingeJoint>());
            StartCoroutine(RemoveChildObject(collision.gameObject.transform.position.normalized * 5));
        }
    }

    IEnumerator RemoveChildObject(Vector3 force)
    {
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        Destroy(GetComponent<HingeJoint>());
        //childObject.transform.SetParent(null);
        //childObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
