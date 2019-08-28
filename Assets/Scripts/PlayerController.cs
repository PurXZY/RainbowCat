using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator m_Animator;
    public float m_Speed = 3.0f;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        // 优先横轴
        moveY = moveX != 0f ? 0f : moveY;
        m_Animator.SetFloat("moveX", moveX);
        m_Animator.SetFloat("moveY", moveY);
        transform.Translate(new Vector2(moveX, moveY) * m_Speed * Time.deltaTime);
    }
}
