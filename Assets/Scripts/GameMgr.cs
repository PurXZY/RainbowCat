using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private SpaceMgr m_SpaceMgr;

    void Start()
    {
        Application.targetFrameRate = 60;

        m_SpaceMgr = GetComponent<SpaceMgr>();

        Invoke("GameStart", 1.0f);
    }

    void GameStart()
    {
        Debug.Log("Game Start");
        // m_SpaceMgr.InitBattleTurnSpace();
        NetworkMgr.Instance.SendHello();
    }
}
