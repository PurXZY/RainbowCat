using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator m_Animator;
    public float m_Speed = 3.0f;
    private bool m_IsMoving = false;
    private Vector2 m_Dir = Vector2.left;
    [SerializeField]
    private GameObject m_BulletObject = null;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void SetMoveState(bool isMoving, Vector2 dir)
    {
        m_IsMoving = isMoving;
        if (isMoving)
            m_Dir = dir;
    }

    void FixedUpdate()
    {
        m_Animator.SetBool("isMoving", m_IsMoving);
        m_Animator.SetFloat("moveX", m_Dir.x);
        m_Animator.SetFloat("moveY", m_Dir.y);
        if (m_IsMoving)
            transform.Translate(m_Dir * m_Speed * Time.deltaTime);
    }

    public void ShootBullet()
    {
        Vector3 bulletPos = transform.position + (Vector3)(m_Dir * 0.2f);
        GameObject bullet = Instantiate(m_BulletObject, bulletPos, Quaternion.identity);
        bullet.GetComponent<BulletController>().Init(gameObject, m_Dir);
    }
}
