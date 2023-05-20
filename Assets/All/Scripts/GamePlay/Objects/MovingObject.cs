using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : ObjectController, Object
{
    override public bool IsMoving()
    {
        return IsObjectMoving();
    }
}
