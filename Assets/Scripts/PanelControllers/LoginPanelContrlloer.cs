using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Usercmd;
using Google.Protobuf;
using System;

public class LoginPanelContrlloer : MonoBehaviour
{
    [SerializeField] private GameObject connectBtn = null;
    [SerializeField] private GameObject loginBtn = null;
    [SerializeField] private GameObject loginInput = null;

    public void OnConnectBtnClick()
    {
        NetworkMgr.Instance.ConnectToServer();
    }

    public void OnLoginBtnClick()
    {
        InputField input = loginInput.GetComponent<InputField>();

        LoginC2SMsg msg = new LoginC2SMsg
        {
            Name = input.text
        };
        NetworkMgr.Instance.GetConnection().SendData((UInt16)UserCmd.LoginReq, msg.ToByteArray());     
    }

    private void Update()
    { 
        if (G.Instance.playerId != 0)
        {
            UIMgr.Instance.ShowReqIntoRoomPanel();
            Destroy(this.gameObject);
            return;
        }
        if (NetworkMgr.Instance.isConnectedToServer)
        {
            connectBtn.SetActive(false);
            loginBtn.SetActive(true);
            loginInput.SetActive(true);
            return;
        }

    }
}
