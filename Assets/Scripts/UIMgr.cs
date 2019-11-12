using Google.Protobuf.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    private Dictionary<string, GameObject> m_EntityHealth = new Dictionary<string, GameObject>();
    [SerializeField] private Transform canvasNode = null;
    private RectTransform canvasRectTransform = null;
    [SerializeField] private GameObject explosionObject = null;
    [SerializeField] private GameObject damageNumObject = null;
    [SerializeField] private GameObject loginPanel = null;
    [SerializeField] private GameObject reqIntoRoomPanel = null;
    [SerializeField] private GameObject accountInfoPanel = null;
    [SerializeField] public OperationPanelController operationPanel = null;
    [SerializeField] private EntityHealthController entityHealthPanel = null;
    [SerializeField] private TurnInfoPanelController turnInfoController;

    public static UIMgr Instance;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        canvasRectTransform = canvasNode.gameObject.GetComponent<RectTransform>();
        loginPanel.GetComponent<LoginPanelContrlloer>().ShowMe();
    }

    [SerializeField] private Text m_TurnInfoText = null;

    public void SetTurnInfoText(uint turn)
    {
        turnInfoController.SetTurnInfoText(turn);
    }

    public static Vector2 WorldToUGUIPosition(RectTransform canvasRectTransform, Vector3 worldPosition)
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPosition);
        return new Vector2(canvasRectTransform.rect.width * viewPos.x, canvasRectTransform.rect.height * viewPos.y);

    }

    public void ShowReqIntoRoomPanel()
    {
        loginPanel.GetComponent<LoginPanelContrlloer>().HideMe();
        reqIntoRoomPanel.GetComponent<ReqIntoRoomPanelController>().ShowMe();
        accountInfoPanel.GetComponent<AccountInfoPanelController>().ShowMe();
    }

    public void OnIntoRoom()
    {
        reqIntoRoomPanel.GetComponent<ReqIntoRoomPanelController>().HideMe();
    }

    public void ShowOperations(RepeatedField<uint> operationSet)
    {
        operationPanel.ShowOperations(operationSet);
    }

    public void NewBattleEntity(uint entityPosIndex)
    {
        entityHealthPanel.CreateHealthBar(entityPosIndex);
    }

    public void ShowChooseOneTarget()
    {
        TurnRoomMgr.Instance.ShowChooseOneTarget();
    }
}
