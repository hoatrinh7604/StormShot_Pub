using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrosshairController : MonoBehaviour
{
    [SerializeField] Transform crossHair;
    [SerializeField] Vector3 rangeStart;
    [SerializeField] Vector3 rangeEnd;

    [SerializeField] float speed;
    private Vector3 target;
    private bool startMoving = false;

    public bool canShoot = false;

    EnemyShootController enemyShootController;
    [SerializeField] float minDelay = 3;
    [SerializeField] float maxDelay = 5;

    [SerializeField] bool isFacingPlayer = false;
    private void Awake()
    {
        enemyShootController = GameElement.Instance.enemyShootController;
        if (isFacingPlayer)
        {
            var playerTran = GameElement.Instance.playerController.transform;
            rangeStart = new Vector3(playerTran.position.x, 13, 0);
            rangeEnd = new Vector3(transform.position.x, 13, 0);
        }
        SetTarget(rangeStart);
    }

    private void Update()
    {
        if (startMoving)
        {
            Moving();
        }
    }

    public void Moving()
    {
        crossHair.position = Vector2.MoveTowards(crossHair.position, target, speed * Time.deltaTime);
        if (!randomDelay)
            StartCoroutine(RandomDelayShoot());
        if (Vector2.Distance(crossHair.position, target) < 0.01f)
        {
            Reverse();
        }
    }

    bool randomDelay = false;
    public void Reverse()
    {
        if (crossHair.position.x >= rangeEnd.x)
        {
            target = rangeStart;
        }
        else if (crossHair.position.x <= rangeStart.x)
        {
            target = rangeEnd;
            //if (!randomDelay)
            //    StartCoroutine(RandomDelayShoot());
        }
    }

    IEnumerator RandomDelayShoot()
    {
        randomDelay = true;
        speed = 1;
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        enemyShootController.CanShoot(true);
        //randomDelay = false;
    }

    Vector3 directPos;
    public void SetDirectionShoot()
    {
        directPos = new Vector3(crossHair.position.x, crossHair.position.y, 0);
    }

    public bool InDirectCanKillPlayer()
    {
        if (Vector3.Distance(crossHair.position, directPos) < 0.01f)
            return true;
        return false;
    }

    public void MovingCrossHair(bool isMoving)
    {
        startMoving = isMoving;
        randomDelay = !isMoving;
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
}
