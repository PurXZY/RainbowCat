using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    private Dictionary<string, GameObject> m_EntityHealth = new Dictionary<string, GameObject>();
    [SerializeField] private Transform canvasNode;
    [SerializeField] private GameObject healthUIObject;


    public static UIMgr Instance;

    private void Awake()
    {
        Instance = this;
    }


    [SerializeField] private Text m_TurnInfoText = null;

    public void SetTurnInfoText(int turn)
    {
        m_TurnInfoText.text = "回合: " + turn;
    }

    public void HealthChanged(string id)
    {
        if (!m_EntityHealth.ContainsKey(id))
        {
            CreateNewHealth(id);
        }
        else
        {
            var healthUI = m_EntityHealth[id];
        }
    }

    private void CreateNewHealth(string id)
    {
        var target = SpaceMgr.Instance.GetEntityById(id);
        var healthUI = Instantiate(healthUIObject, canvasNode);
        m_EntityHealth.Add(id, healthUI);
        healthUI.GetComponent<HealthUIController>().SetOwner(id, canvasNode.gameObject.GetComponent<RectTransform>());
    }

    public static Vector2 WorldToUGUIPosition(RectTransform canvasRectTransform, Vector3 worldPosition)
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPosition);
        return new Vector2(canvasRectTransform.rect.width * viewPos.x, canvasRectTransform.rect.height * viewPos.y);

    }
}
