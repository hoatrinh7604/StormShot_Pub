using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsRemaining : MonoBehaviour
{
    [SerializeField] GameObject bulletObj;
    [SerializeField] Sprite[] sprites;

    [SerializeField] GameObject[] listBullets;

    private int remaining;

    private void Start()
    {
        
    }

    public void SpawBulletFollowQuantity(int quantity)
    {
        DestroyAllBullet();
        remaining = quantity;
        listBullets = new GameObject[quantity];
        for(int i = 0; i< remaining; i++)
        {
            var obj = Instantiate(bulletObj, transform);
            obj.GetComponent<Image>().sprite = sprites[0];
            listBullets[i] = obj;
        }
    }

    public void ReduceQuantity()
    {
        remaining--;
        listBullets[remaining].gameObject.GetComponent<Image>().sprite = sprites[1];
    }

    public void DestroyAllBullet()
    {
        for(int i = transform.childCount - 1; i>=0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void TestMode(int quantity)
    {
        remaining = quantity;
        SpawBulletFollowQuantity(remaining);
    }
}
