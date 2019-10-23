using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntityController : MonoBehaviour
{
    public string m_id = null;
    public float m_BattleSpeed = 0.0f;
    public bool m_IsTeamLeft = true;

    public void SetBattleData(string id, float speed, bool isTeamLeft)
    {
        m_id = id;
        m_BattleSpeed = speed;
        m_IsTeamLeft = isTeamLeft;
    }

    public void BeginMyTurn()
    {

    }
}

/// <summary>
/// 从大到小排序
/// </summary>
public class CompareBattleEntityBySpeed : IComparer<BattleEntityController>
{
    public int Compare(BattleEntityController x, BattleEntityController y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        if (x.m_BattleSpeed > y.m_BattleSpeed) return -1;
        if (x.m_BattleSpeed < y.m_BattleSpeed) return 1;
        return 0;
    }
}