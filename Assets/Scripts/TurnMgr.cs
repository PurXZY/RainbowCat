using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMgr
{
    private int m_BigTurnInfo = 0;
    private static TurnMgr instance;
    private List<string> m_SmallTurnOrder = new List<string>();
    private int m_SmallTurnOrderIndex = 0;

    public static TurnMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TurnMgr();
            }
            return instance;
        }
    }

    public void BeginTurn()
    {
        SetTurnInfo(1);
    }

    private void SetTurnInfo(int turn)
    {
        m_BigTurnInfo = turn;
        UIMgr.Instance.SetTurnInfoText(m_BigTurnInfo);
        SortBattleEntitySpeed();
        AssignNextEntityTurn();
    }

    /// <summary>
    /// 排序本轮大回合所有entity的小回合顺序
    /// </summary>
    private void SortBattleEntitySpeed()
    {
        m_SmallTurnOrder.Clear();
        m_SmallTurnOrderIndex = 0;
        var ret = SpaceMgr.Instance.GetAllBattleEntities();
        ret.Sort(new CompareBattleEntityBySpeed());
        foreach(var i in ret)
        {
            m_SmallTurnOrder.Add(i.m_id);
        }
    }

    private void AssignNextEntityTurn()
    {

    }

    /// <summary>
    /// 指定entity开始自己的小回合
    /// </summary>
    /// <param name="id"></param>
    private void AssignEntityTurn(string id)
    {

    }

    /// <summary>
    /// entity结束自己小回合
    /// </summary>
    /// <param name="id"></param>
    public void EntityEndTurn(string id)
    {

    }
}
