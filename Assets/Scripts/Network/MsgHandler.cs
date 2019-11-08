using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usercmd;

public class MsgHandler
{
    public MsgHandler()
    {
        var mgr = NetworkMgr.Instance;
        mgr.RegisterMsgHandler((int)CmdType.LoginRes, HandleLoginMsg);
        mgr.RegisterMsgHandler((int)CmdType.IntoRoomRes, HandleIntoRoomMsg);
        mgr.RegisterMsgHandler((int)CmdType.SyncAllBattleEntities, HandleCreateAllBattleEntities);
        mgr.RegisterMsgHandler((int)CmdType.TurnInfo, HandleTurnInfo);
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

    private void HandleCreateAllBattleEntities(NetMsg msg)
    {
        var body = SyncAllBattleEntitiesS2CMsg.Parser.ParseFrom(msg.GetMsgData());
        TurnRoomMgr.Instance.CreateAllBattleEntities(body.Entities);
    }

    private void HandleTurnInfo(NetMsg msg)
    {
        var body = TurnInfoS2CMsg.Parser.ParseFrom(msg.GetMsgData());
        Debug.Log("TurnInfo: " + body.BigTurnIndex + " " + body.SmallTurnIndex + " " + body.CurEntityPosIndex);
        UIMgr.Instance.SetTurnInfoText(body.BigTurnIndex);
        TurnRoomMgr.Instance.AssignTargetTurn(body.CurEntityPosIndex);
        UIMgr.Instance.ShowOperations(body.OperationSet);
    }
}
