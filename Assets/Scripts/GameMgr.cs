using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    TurnMgr m_TurnMgr;

    void Start()
    {
        Application.targetFrameRate = 60;
        m_TurnMgr = GetComponent<TurnMgr>();

        Invoke("GameStart", 1.0f);
    }

    void GameStart()
    {
        Debug.Log("Game Start");
        m_TurnMgr.GameStart();
    }
}
