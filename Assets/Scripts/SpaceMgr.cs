﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMgr : MonoBehaviour
{
    public static SpaceMgr Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject BattleEntity = null;

    private GameObject m_BattleEntitiesParentNode;
    private Transform m_LeftTeamParentTransform;
    private Transform m_RightTeamParentTransform;
    public bool IsGameOver = false;

    private Dictionary<string, GameObject> m_entities = new Dictionary<string, GameObject>();

    private void Start()
    {
        m_BattleEntitiesParentNode = GameObject.FindWithTag("BattleEntities");
        m_LeftTeamParentTransform = m_BattleEntitiesParentNode.transform.Find("TeamLeft").transform;
        m_RightTeamParentTransform = m_BattleEntitiesParentNode.transform.Find("TeamRight").transform;
    }

    public void InitBattleTurnSpace()
    {
        InitAllBattleEntities();
        TurnMgr.Instance.BeginTurn();
    }

    private void InitAllBattleEntities()
    {
        InitBattleEntity("left1", new Vector2(-7, -3), true);
        InitBattleEntity("left2", new Vector2(-5.5f, -3f), true);
        InitBattleEntity("left3", new Vector2(-4, -3), true);
        InitBattleEntity("right1", new Vector2(4, -3), false);
        InitBattleEntity("right2", new Vector2(5.5f, -3f), false);
        InitBattleEntity("right3", new Vector2(7, -3), false);
    }

    private void InitBattleEntity(string id, Vector2 pos, bool isTeamLeft)
    {
        Transform parentTransform = isTeamLeft ? m_LeftTeamParentTransform : m_RightTeamParentTransform;
        var entity = Instantiate(BattleEntity, pos, Quaternion.identity, parentTransform);
        RegisterEntity(id, entity);
        var speed = Random.Range(1.0f, 10.0f);
        var entityController = entity.GetComponent<BattleEntityController>();
        entityController.SetBattleData(id, 10.0f, speed, isTeamLeft, 5.0f, 1.5f);
        Debug.Log(string.Format("new Entity:{0} speed{1}", id, speed));
    }

    public List<BattleEntityController> GetAllBattleEntities()
    {
        List<BattleEntityController> ret = new List<BattleEntityController>();
        ret.AddRange(GetTeamLeftMemberController());
        ret.AddRange(GetTeamRightMemberController());
        return ret;
    }

    public List<BattleEntityController> GetTeamLeftMemberController()
    {
        List<BattleEntityController> ret = new List<BattleEntityController>();
        foreach (Transform child in m_LeftTeamParentTransform.transform)
        {
            ret.Add(child.GetComponent<BattleEntityController>());
        }
        return ret;
    }

    public List<BattleEntityController> GetTeamRightMemberController()
    {
        List<BattleEntityController> ret = new List<BattleEntityController>();
        foreach (Transform child in m_RightTeamParentTransform.transform)
        {
            ret.Add(child.GetComponent<BattleEntityController>());
        }
        return ret;
    }

    public GameObject GetEntityById(string id)
    {
        if (m_entities.ContainsKey(id))
            return m_entities[id];
        return null;
    }

    private void RegisterEntity(string id, GameObject entity)
    {
        m_entities.Add(id, entity);
    }

    public void RemoveEntity(string id)
    {
        if (m_entities.ContainsKey(id))
            m_entities.Remove(id);
    }

    public GameObject GetRandomEntityByTeam(bool isTeamLeft)
    {
        List<BattleEntityController> ret;
        if (isTeamLeft)
        {
            ret = GetTeamLeftMemberController();
        }
        else
        {
            ret = GetTeamRightMemberController();
        }
        int len = ret.Count;
        if (len == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, len);
        return ret[randomIndex].gameObject;
    }
}
