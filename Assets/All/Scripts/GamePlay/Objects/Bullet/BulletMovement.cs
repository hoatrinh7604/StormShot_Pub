using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody2D rb2D;
    Rigidbody rb;
    [SerializeField] float speed;

    [SerializeField] Transform crosshair;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector2(20 * Time.deltaTime * speed, 20 * Time.deltaTime * speed));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddForce(crosshair);
        }
    }

    public void AddForce(Transform crosshairT)
    {
        Vector2 direction = new Vector2(crosshairT.position.x - this.transform.position.x, crosshairT.position.y - this.transform.position.y);
        rb.AddForce(new Vector2(20 * direction.x * Time.deltaTime * speed, 20 * direction.y * Time.deltaTime * speed));
    }

}
