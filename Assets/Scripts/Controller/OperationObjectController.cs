using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationObjectController : MonoBehaviour
{
    [SerializeField] private Text m_name = null;
    public uint operationId = 0;
    private bool isClicked = false;


    private void Start()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    public void Init(uint operationId)
    {
        this.operationId = operationId;
        var name = GData.Instance.GetOperationData(operationId).name;
        m_name.text = name;
    }

    public void ShowMe()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    public void HideMe()
    {
        GetComponent<CanvasGroup>().alpha = 0.2f;
    }

    public void OnClick()
    {
        if (isClicked)
        {
            CancelClick();
        }
        else
        {
            FirstClick();
        }
        isClicked = !isClicked;
    }

    private void FirstClick()
    {
        UIMgr.Instance.operationPanel.HideOthers(operationId);
        var image = GetComponent<Image>();
        image.color = Color.red;
        ShowSkillTarget();
    }

    private void CancelClick()
    {
        UIMgr.Instance.operationPanel.HideOthers(operationId, true);
        var image = GetComponent<Image>();
        image.color = new Color(0.26f, 0.73f, 0.55f, 1f);
    }

    private void ShowSkillTarget()
    {
        var enemyType = GData.Instance.GetOperationData(operationId).enemyType;
        switch (enemyType)
        {
            case 1:
                UIMgr.Instance.ShowChooseOneTarget();
                break;
            default:
                break;
        }
    }
}
