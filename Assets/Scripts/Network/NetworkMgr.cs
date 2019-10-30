using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Usercmd;

public class NetworkMgr : MonoBehaviour
{
    public static NetworkMgr Instance;
    private NetworkConnection m_Connection;
    public bool isConnectedToServer = false;

    public Queue<NetMsg> msgQueue = new Queue<NetMsg>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        m_Connection = new NetworkConnection();
    }

    void Update()
    {
        if (msgQueue.Count == 0)
            return;
        lock(msgQueue)
        {
            while (msgQueue.Count != 0)
            {
                var msg = msgQueue.Dequeue();
                DispatchNetMsg(msg);
            }
        }
    }

    public NetworkConnection GetConnection()
    {
        return m_Connection;
    }

    private void OnApplicationQuit()
    {
        if (m_Connection != null)
        {
            m_Connection.CloseConnection();
        }
    }

    public void ConnectToServer()
    {
        m_Connection.InitConnection();
    }

    private void DispatchNetMsg(NetMsg msg)
    {
        var msgId = msg.GetMsgId();
        switch (msgId)
        {
            case (int)UserCmd.LoginRes:
                var body = LoginS2CMsg.Parser.ParseFrom(msg.GetMsgData());
                G.Instance.playerId = (int)body.PlayerId;
                UIMgr.Instance.ShowReqIntoRoomPanel();
                return;
            default:
                Debug.Log("unknown msg id: " + msgId);
                break;
        }
    }
}
