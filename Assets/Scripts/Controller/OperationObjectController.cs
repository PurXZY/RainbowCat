using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OperationObjectController : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Text m_name = null;
    public uint operationId = 0;
    private bool isClicked = false;
    private RectTransform rectTransform;


    private void Start()
    {
        //var btn = GetComponent<Button>();
        //btn.onClick.AddListener(OnClick);
        rectTransform = GetComponent<RectTransform>();
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
        UIMgr.Instance.CancelAllShowTarget();
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("开始拖拽");
        UIMgr.Instance.operationPanel.HideOthers(operationId);
        var image = GetComponent<Image>();
        image.color = Color.red;
        ShowSkillTarget();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.position = pos;

        //从摄像机发出到点击坐标的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //划出射线，只有在scene视图中才能看到
            Debug.DrawLine(ray.origin, hitInfo.point);
            GameObject gameObj = hitInfo.collider.gameObject;
            Debug.Log("click object name is " + gameObj.name);
            //当射线碰撞目标为boot类型的物品，执行拾取操作
            if (gameObj.tag == "boot")
            {
                Debug.Log("pickup!");
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("结束拖拽");
        UIMgr.Instance.operationPanel.HideOthers(operationId, true);
        var image = GetComponent<Image>();
        image.color = new Color(0.26f, 0.73f, 0.55f, 1f);
        UIMgr.Instance.CancelAllShowTarget();
        LayoutRebuilder.ForceRebuildLayoutImmediate(UIMgr.Instance.operationPanel.GetComponent<RectTransform>());
    }
}
