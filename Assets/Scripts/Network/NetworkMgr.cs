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
    public bool isConnectedToServer = false;

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

    public void SendHello()
    {
        LoginC2SMsg a = new LoginC2SMsg
        {
            Name = "xzy"
        };
        m_Connection.SendData((UInt16)UserCmd.LoginReq, a.ToByteArray());
    }
}
