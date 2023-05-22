using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserScript : MonoBehaviour
{
    //[SerializeField] int maxBounce = 5;
    //[SerializeField] LineRenderer lineRenderer;
    //[SerializeField] Transform startPoint;
    //[SerializeField] bool reflex;

    //[SerializeField] Transform target;

    //Vector3 direct;
    //private void Awake()
    //{
    //    direct = (target.position - startPoint.position).normalized;
    //    //lineRenderer.SetPosition(0, new Vector3(startPoint.position.x, startPoint.position.y, 0));
    //    lineRenderer.SetPosition(0, startPoint.position);
    //}

    //private void Update()
    //{
    //    //Vector3 line1 = new Vector3(startPoint.position.x, startPoint.position.y, 0);

    //    //Vector3 line2Temp = target.position + 100 * (target.position - startPoint.position).normalized;
    //    //Vector3 line2 = new Vector3(line2Temp.x, line2Temp.y, 0);
    //    ////lineRenderer.SetPosition(1, line2);
    //    //CastLaser(line1, line2);
    //    CastLaser(transform.position, -transform.forward);
    //}

    //public void CastLaser(Vector3 position, Vector3 direction)
    //{
    //    lineRenderer.SetPosition(0, transform.position);
    //    //lineRenderer.SetPosition(1, direction);

    //    for(int i = 0; i< maxBounce; i++)
    //    {
    //        Ray ray = new Ray(position, direction);
    //        RaycastHit hit;

    //        if(Physics.Raycast(ray, out hit, 300, 1))
    //        {
    //            position = hit.point;
    //            direction = Vector3.Reflect(direction, hit.normal);
    //            lineRenderer.SetPosition(i + 1, new Vector2(hit.point.x, hit.point.y));

    //            if(reflex)
    //            {
    //                for(int j = i+1; j<= maxBounce; j++)
    //                {
    //                    lineRenderer.SetPosition(j, hit.point);
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    [SerializeField] Transform shootPoint;
    [SerializeField] Transform targetPoint;

    EnemyShootController enemyShootController;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        enemyShootController = GameElement.Instance.enemyShootController;
        SetShootPoint(enemyShootController.GetShootPoint());
    }

    private bool hittingPlayer = false;
    private bool hittingCharacter = false;
    private void Update()
    {
        ray = new Ray(Vector3Z(shootPoint.position), 100*(Vector3Z(targetPoint.position) - Vector3Z(shootPoint.position)).normalized);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, Vector3Z(shootPoint.position));
        float remainingLength = maxLength;

        if (enemyShootController.isDeath()) return;
        hittingPlayer = false;
        hittingCharacter = false;
        for (int i = 0; i< reflections; i++)
        {
            if(Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(hit.point.x, hit.point.y, 0));
                if (hit.collider.GetComponent<PoisionObject>())
                {
                    hittingCharacter = true;
                    break;
                }
                if (hit.collider.tag == GameContracts.PLAYER_TAG)
                {
                    enemyShootController.CheckTheDirectCanKillPlayer();
                    hittingPlayer = true;
                    break;
                }
                //else if (hit.collider.tag == GameContracts.ENEMY_TAG || hit.collider.tag == GameContracts.HOSTAGE_TAG || hit.collider.tag == GameContracts.PLAYER_TAG)
                else if (hit.collider.tag == GameContracts.ENEMY_TAG || hit.collider.tag == GameContracts.HOSTAGE_TAG || hit.collider.tag == GameContracts.PLAYER_TAG)
                {
                    hittingCharacter = true;
                    break;
                }

                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(Vector3Z(hit.point), Vector3.Reflect(Vector3Z(ray.direction), Vector3Z(hit.normal)));
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, Vector3Z(ray.origin + ray.direction * remainingLength));
            }
        }

        if (enemyShootController.IsCanShoot())
        {
            if (enemyShootController.CanKillPlayer() && enemyShootController.HaveDirectToKillPlayer() && hittingPlayer)
            {
                enemyShootController.Shooting();
                enemyShootController.StopAiming();
            }
            else
            {
                if (!hittingCharacter)
                {
                    enemyShootController.Shooting();
                    enemyShootController.StopAiming();
                }
            }
        }
    }

    private Vector3 Vector3Z(Vector3 vec)
    {
        return new Vector3(vec.x, vec.y, 0);
    }

    IEnumerator Shooting()
    {
        //yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        yield return null;
        enemyShootController.Shooting();
    }

    public void SetShootPoint(Transform shootP)
    {
        shootPoint = shootP;
    }

    public void Shoot()
    {
        //shootToKill = enemyShootController.CanKillPlayer();
    }

    public void GenerateMeshCollider()
    {
        MeshCollider collider = GetComponent<MeshCollider>();

        if(collider == null)
        {
            collider = gameObject.AddComponent<MeshCollider>();
        }

        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
