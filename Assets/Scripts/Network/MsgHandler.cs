using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usercmd;

public class MsgHandler
{
    public MsgHandler()
    {
        var mgr = NetworkMgr.Instance;
        mgr.RegisterMsgHandler((int)UserCmd.LoginRes, HandleLoginMsg);
        mgr.RegisterMsgHandler((int)UserCmd.IntoRoomRes, HandleIntoRoomMsg);
    }

    private void HandleLoginMsg(NetMsg msg)
    {
        var body = LoginS2CMsg.Parser.ParseFrom(msg.GetMsgData());
        G.Instance.playerId = (int)body.PlayerId;
        UIMgr.Instance.ShowReqIntoRoomPanel();
    }

    private void HandleIntoRoomMsg(NetMsg msg)
    {
        var body = IntoRoomS2cMsg.Parser.ParseFrom(msg.GetMsgData());
        Debug.Log("room id " + body.RoomId);
        UIMgr.Instance.OnIntoRoom();
    }
}
