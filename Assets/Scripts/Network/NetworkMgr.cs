using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Usercmd;
using Google.Protobuf;

public class NetworkMgr : MonoBehaviour
{
    public static NetworkMgr Instance;
    private NetworkConnection m_Connection;
    public Action<bool> connectCallback;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        m_Connection = new NetworkConnection();
        m_Connection.InitConnection();
    }

    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        if (m_Connection != null)
        {
            m_Connection.CloseConnection();
        }
    }

    public void SendHello()
    {
        LoginC2SMsg a = new LoginC2SMsg
        {
            Name = "xzy"
        };
        m_Connection.SendData((UInt16)UserCmd.LoginReq, a.ToByteArray());
    }
}
