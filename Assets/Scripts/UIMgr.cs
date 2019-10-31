using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    private Dictionary<string, GameObject> m_EntityHealth = new Dictionary<string, GameObject>();
    [SerializeField] private Transform canvasNode = null;
    private RectTransform canvasRectTransform = null;
    [SerializeField] private GameObject healthUIObject = null;
    [SerializeField] private GameObject winShowText = null;
    [SerializeField] private GameObject explosionObject = null;
    [SerializeField] private GameObject damageNumObject = null;
    [SerializeField] private GameObject loginPanel = null;
    [SerializeField] private GameObject reqIntoRoomPanel = null;
    [SerializeField] private GameObject accountInfoPanel = null;


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

    public void SetTurnInfoText(int turn)
    {
        m_TurnInfoText.text = "回合: " + turn;
    }

    public static Vector2 WorldToUGUIPosition(RectTransform canvasRectTransform, Vector3 worldPosition)
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPosition);
        return new Vector2(canvasRectTransform.rect.width * viewPos.x, canvasRectTransform.rect.height * viewPos.y);

    }

    public void GameOver(bool isTeamLeftWin)
    {
        winShowText.GetComponent<Text>().text = isTeamLeftWin ? "Left Win" : "Right Win";
        winShowText.SetActive(true);
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
}
