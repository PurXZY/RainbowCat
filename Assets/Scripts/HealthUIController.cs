using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    private GameObject m_owner;
    private RectTransform m_canvas;
    private Image m_healthReal;

    void Start()
    {
        m_healthReal = transform.Find("HealthReal").GetComponent<Image>();
    }

    private void LateUpdate()
    {
        var pos = (Vector2)m_owner.transform.position + new Vector2(0.0f, 1.5f);
        transform.position = UIMgr.WorldToUGUIPosition(m_canvas, pos);
    }

    public void SetOwner(string id, RectTransform canvas)
    {
        // m_owner = SpaceMgr.Instance.GetEntityById(id);
        m_canvas = canvas;
    }

    public void RefershHealth()
    {
        var ownerController = m_owner.GetComponent<BattleEntityController>();
        // float ratio = ownerController.m_CurHealth / ownerController.m_MaxHealth;
        float ratio = 1;
        m_healthReal.fillAmount = ratio;
    }
}
