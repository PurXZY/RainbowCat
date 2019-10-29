using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G
{
    private static G instance;
    public static G Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new G();
            }
            return instance;
        }
    }

    public int playerId = 0;
}
