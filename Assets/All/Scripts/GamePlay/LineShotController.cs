using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShotController : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform crossHair;
    [SerializeField] GameObject effect;

    private bool startDraw = true;
    private Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        //shooting = GameElement.Instance.shooting;
        //shootPoint = shooting.GetShootPoint();
        //line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootPoint == null) return;
        if (startDraw && !GameplayController.Instance.IsPauseGame())
        {
            //shootPoint.transform.localPosition = Vector3.zero;
            Vector3 line1 = new Vector3(shootPoint.position.x, shootPoint.position.y, 0);

            Vector3 line2Temp = crossHair.position + 100 * (crossHair.position - shootPoint.position).normalized;
            Vector3 line2 = new Vector3(line2Temp.x, line2Temp.y, 0);
            line.SetPosition(0, line1);
            line.SetPosition(1, line2);
            if(effect)
                effect.transform.position = new Vector3(line2.x, line2.y, 0);
        }
    }

    public void SetLine(Transform shotPoint, Transform crossHair)
    {
        this.shootPoint = shotPoint;
        this.crossHair = crossHair;

        startDraw = true;
    }

    public void ShowLine(bool isDisplay)
    {
        line.enabled = isDisplay;
        if(effect)
            effect.SetActive(isDisplay);
    }

    public void SetShootPoint(Transform point)
    {
        shootPoint = point;
    }
}
