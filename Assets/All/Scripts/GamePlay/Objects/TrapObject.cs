using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : ObjectController, Object
{
    public override void SetInfo()
    {
        ID = (int)ItemObject.TRAP;
        type = (int)Barrel.HARD;
    }
}
