using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]float m_speed = 3.0f;
    MoveState m_moveState = MoveState.idle;
    Vector2 m_dir = Vector2.up;
    Animator m_animator;
    SpriteRenderer m_spriteRenderer;
    List<Vector2> m_move_keys = new List<Vector2>();
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_move_keys.Add(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_move_keys.Add(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_move_keys.Add(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_move_keys.Add(Vector2.right);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            m_move_keys.Remove(Vector2.up);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            m_move_keys.Remove(Vector2.down);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            m_move_keys.Remove(Vector2.left);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_move_keys.Remove(Vector2.right);
        }

        if (m_move_keys.Count != 0)
        {
            m_dir = m_move_keys[0];
            m_moveState = MoveState.moving;
        }
        else
        {
            m_moveState = MoveState.idle;
        }
        m_animator.Play(GetAnimationNameByMoveState());
    }

    void FixedUpdate()
    {
        if (m_moveState == MoveState.moving)
        {
            transform.Translate(m_dir * m_speed * Time.deltaTime);
        }
    }

    string GetAnimationNameByMoveState()
    {
        if (m_moveState == MoveState.idle)
            return "redboy_idle";
        else if (m_dir == Vector2.up)
            return "redboy_walk_up";
        else if (m_dir == Vector2.down)
            return "redboy_walk_down";
        else if (m_dir == Vector2.left)
        {
            m_spriteRenderer.flipX = false;
            return "redboy_walk_left";
        }
        else if (m_dir == Vector2.right)
        {
            m_spriteRenderer.flipX = true;
            return "redboy_walk_left";
        }
        else 
             return "redboy_idle";
    }
}
