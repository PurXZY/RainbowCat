using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMgr : MonoBehaviour
{
    public static SpaceMgr Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject BattleEntity;

    private GameObject m_BattleEntitiesParentNode;
    private Transform m_LeftTeamParentTransform;
    private Transform m_RightTeamParentTransform;

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
        EntityMgr.Instance.RegisterEntity(id, entity);
        var speed = Random.Range(1.0f, 10.0f);
        var entityController = entity.GetComponent<BattleEntityController>();
        entityController.SetBattleData(id, speed);
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
}
