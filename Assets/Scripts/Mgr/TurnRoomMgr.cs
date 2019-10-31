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
    private Dictionary<uint, BattleEntityController> m_entities = new Dictionary<uint, BattleEntityController>();

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
        controller.Init(entityData);
        m_entities.Add(posIndex, controller);
    }
}
