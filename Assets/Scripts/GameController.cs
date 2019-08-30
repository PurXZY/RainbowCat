using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    
    private PlayerController m_PlayerController;

    void Start()
    {
        m_PlayerController = FindPlayer().GetComponent<PlayerController>();
    }

    void Update()
    {
        MoveTick();
        ShootTick();
    }

    private GameObject FindPlayer()
    {
        var targets = GameObject.FindGameObjectsWithTag("Player");
        if (targets.Length > 0)
            return targets[0];
        return null;
    }

    private void MoveTick()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        bool isMoving = true;
        Vector2 dir = Vector2.zero;
        if (Mathf.Abs(moveX) > 0.01f)
        {
            dir = moveX > 0.01f ? Vector2.right : Vector2.left;
        }
        else if (Mathf.Abs(moveY) > 0.01f)
        {
            dir = moveY > 0.01f ? Vector2.up : Vector2.down;
        }
        else
        {
            isMoving = false;
        }

        m_PlayerController.SetMoveState(isMoving, dir);
    }

    private void ShootTick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_PlayerController.ShootBullet();
        }
    }
}
