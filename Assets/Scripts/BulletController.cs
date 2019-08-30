using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject m_Owner = null;
    private Vector2 m_Dir = Vector2.zero;
    private float m_Speed = 10.0f;
    private Rigidbody2D m_Rigidbody;
    [SerializeField]
    private GameObject m_ExplosionObject = null;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
 
    public void Init(GameObject caster, Vector2 dir)
    {
        m_Owner = caster;
        m_Dir = dir;
        m_Rigidbody.velocity = dir * m_Speed;
        Destroy(gameObject, 2.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TagCheckManager.IsWall(collision.gameObject))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(m_ExplosionObject, transform.position, Quaternion.identity);
        Destroy(explosion, 1.0f);
    }
}
