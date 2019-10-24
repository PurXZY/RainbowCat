using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    private GameObject m_owner;
    private RectTransform m_canvas;

    void Start()
    {
        
    }

    private void LateUpdate()
    {
        var pos = (Vector2)m_owner.transform.position + new Vector2(0.0f, 1.5f);
        transform.position = UIMgr.WorldToUGUIPosition(m_canvas, pos);
    }

    public void SetOwner(string id, RectTransform canvas)
    {
        m_owner = SpaceMgr.Instance.GetEntityById(id);
        m_canvas = canvas;
    }
}
