using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : ObjectController
{
    public override void SetInfo()
    {
        ID = (int)ItemObject.SAW;
        type = (int)Barrel.HARD;
    }
}
