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
    private Dictionary<int, Action<NetMsg>> msgDispatcher = new Dictionary<int, Action<NetMsg>>();
    private MsgHandler handler;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        m_Connection = new NetworkConnection();
        handler = new MsgHandler();
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
        if (msgDispatcher.ContainsKey(msgId))
        {
            msgDispatcher[msgId]?.Invoke(msg);
        }
        else
        {
            Debug.Log("unknown msg id: " + msgId);
        }
    }

    public void RegisterMsgHandler(int msgId, Action<NetMsg> action)
    {
        if (msgDispatcher.ContainsKey(msgId))
        {
            Debug.Log("RegisterMsgHandler same msg id " + msgId);
            return;
        }
        msgDispatcher[msgId] = action;
    }
}
