using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

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
}
