using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usercmd;

public class BattleEntityController : MonoBehaviour
{
    private float m_MoveSpeed = 0.0f;
    private float m_MaxHealth = 0.0f;
    private float m_CurHealth = 0.0f;
    private float m_PhysicalAttack = 0.0f;
    private float m_PhysicalDefend = 0.0f;
    private float m_MagicAttack = 0.0f;
    private float m_MagicDefend = 0.0f;

    private Vector2 m_BirthPos;
    private Vector2 m_MoveToPos;

    public void Init(Usercmd.BattleEntity entityData)
    {
        m_BirthPos = transform.position;
        m_MaxHealth = entityData.Health;
        m_CurHealth = entityData.Health;
        m_MoveSpeed = entityData.MoveSpeed;
        m_PhysicalAttack = entityData.PhysicalAttack;
        m_PhysicalDefend = entityData.PhysicalDefend;
        m_MagicAttack = entityData.MagicAttack;
        m_MagicDefend = entityData.MagicDefend;
    }
}
