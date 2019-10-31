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
            CreateBattleEntity(entity.PosIndex, entity.EntityType);
        }
    }

    private void CreateBattleEntity(uint posIndex, uint entityType)
    {
        var pos = GData.Instance.PosIndexMap[posIndex];
        var entity = Instantiate(battleEntityObject, pos, Quaternion.identity, battleEntityParent);
        m_entities.Add(posIndex, entity.GetComponent<BattleEntityController>());
    }
}
