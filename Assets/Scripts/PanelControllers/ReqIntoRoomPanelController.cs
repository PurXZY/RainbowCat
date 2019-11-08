using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usercmd;
using System;
using Google.Protobuf;

public class ReqIntoRoomPanelController : MonoBehaviour
{
    public void OnBtnClick()
    {
        IntoRoomC2SMsg msg = new IntoRoomC2SMsg{};
        NetworkMgr.Instance.GetConnection().SendData((UInt16)CmdType.IntoRoomReq, msg.ToByteArray());
    }

    public void ShowMe()
    {
        this.gameObject.SetActive(true);
    }

    public void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
