using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObject : ObjectController, Object
{
    override public bool IsMoving()
    {
        return IsObjectMoving();
    }
}
