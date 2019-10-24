using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntityController : MonoBehaviour
{
    public string m_id = null;
    public float m_BattleSpeed = 0.0f;
    public bool m_IsTeamLeft = true;
    public float m_MaxHealth = 0.0f;
    public float m_CurHealth = 0.0f;

    private bool m_IsMyTurn = false;
    private Vector2 m_BirthPos;
    private Vector2 m_MoveToPos;

    public void SetBattleData(string id, float health, float speed, bool isTeamLeft)
    {
        m_BirthPos = transform.position;
        m_id = id;
        m_MaxHealth = health;
        m_CurHealth = health;
        m_BattleSpeed = speed;
        m_IsTeamLeft = isTeamLeft;

        HealthChanged();
    }

    private void HealthChanged()
    {
        UIMgr.Instance.HealthChanged(m_id);
    }

    public void BeginMyTurn()
    {
        m_IsMyTurn = true;
        if (m_IsTeamLeft)
        {
            DoAI();
        }
        else
        {
            DoAI();
            // Invoke("EndMyTurn", 1.0f);
        }
    }

    private void DoAI()
    {
        var target = SpaceMgr.Instance.GetRandomEntityByTeam(!m_IsTeamLeft);
        m_MoveToPos = target.transform.position;
        StartCoroutine(Patrol(transform.position, m_MoveToPos, 1.0f));
    }

    private void EndMyTurn()
    {
        m_IsMyTurn = false;
        TurnMgr.Instance.EntityEndTurn();
    }

    //在time时间内移动物体
    private IEnumerator Patrol(Vector2 startPos, Vector2 endPos, float time)
    {
        var dur = 0.0f;
        while (dur <= time)
        {
            dur += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, endPos, dur / time);
            yield return null;
        }
        dur = 0.0f;
        while (dur <= time)
        {
            dur += Time.deltaTime;
            transform.position = Vector2.Lerp(endPos, startPos, dur / time);
            yield return null;
        }
        EndMyTurn();
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