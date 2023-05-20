using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : ObjectController
{
    public override void SetInfo()
    {
        ID = (int)ItemObject.BOX;
        type = (int)Barrel.HARD;
    }
}
