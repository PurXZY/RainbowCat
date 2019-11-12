using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Collections;

public class TurnRoomMgr : MonoBehaviour
{
    public static TurnRoomMgr Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Transform battleEntityParent = null;
    [SerializeField] private GameObject battleEntityObject = null;
    public Dictionary<uint, BattleEntityController> m_entities = new Dictionary<uint, BattleEntityController>();

    public void CreateAllBattleEntities(RepeatedField<Usercmd.BattleEntity> entities)
    {
        foreach (var entity in entities)
        {
            CreateBattleEntity(entity);
        }
    }

    private void CreateBattleEntity(Usercmd.BattleEntity entityData)
    {
        var posIndex = entityData.PosIndex;
        var pos = GData.Instance.PosIndexMap[posIndex];
        var entity = Instantiate(battleEntityObject, pos, Quaternion.identity, battleEntityParent);
        var controller = entity.GetComponent<BattleEntityController>();
        m_entities.Add(posIndex, controller);
        controller.Init(entityData);
    }

    public BattleEntityController GetEntityController(uint pos)
    {
        return m_entities[pos];
    }

    public void AssignTargetTurn(uint curEntityPosIndex)
    {
        var controller = GetEntityController(curEntityPosIndex);
        controller.ShowMyTurn();
    }

    public void ShowChooseOneTarget()
    {
        foreach (var kvp in m_entities)
        {
            var c = kvp.Value;
            c.ShowTargetChoose(false);
        }
    }
}
