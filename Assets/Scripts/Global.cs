using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState
{
    idle = 1,
    moving = 2,
}


public class TagCheckManager
{
    public static bool IsEnemy(GameObject target)
    {
        if (target.CompareTag("Enemy"))
            return true;
        return false;
    }
}

