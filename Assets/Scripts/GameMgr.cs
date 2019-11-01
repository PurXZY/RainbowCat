using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
        var i = GData.Instance;
    }
}
