using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    SpriteRenderer m_spriteRenderer;
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
