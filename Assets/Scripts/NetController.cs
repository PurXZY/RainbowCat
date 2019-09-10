using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System;

public class NetController : MonoBehaviour
{
    private Socket serverSocket;
    private IPAddress m_Ip;
    private IPEndPoint m_IpEnd;
    private Thread m_Thread;

    private byte[] recvData = new byte[1024]; //接收的数据，必须为字节
    private byte[] sendData = new byte[1024]; //发送的数据，必须为字节
    private int recvLen; //接收的数据长度

    void Start()
    {
        m_Ip = IPAddress.Parse("127.0.0.1");
        m_IpEnd = new IPEndPoint(m_Ip, 12233);

        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Connect(m_IpEnd);

        m_Thread = new Thread(new ThreadStart(SocketReceive));
        m_Thread.Start();
    }

    void SocketReceive()
    {
        SocketSend("hello server");
        //不断接收服务器发来的数据
        while (true)
        {
            recvData = new byte[1024];
            recvLen = serverSocket.Receive(recvData);
            if (recvLen == 0)
            {
                serverSocket.Close();
                continue;
            }
        }
    }

    void SocketSend(string sendStr)
    {
        int msgLen = Encoding.UTF8.GetByteCount(sendStr);
        byte[] data = new byte[4 + msgLen];
        //把长度转成字节数组 大端字节
        byte[] lenbytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(msgLen));
        //把消息转为字节数组
        byte[] bodybytes = Encoding.UTF8.GetBytes(sendStr);
        //将len添加到bytes数组中
        Array.Copy(lenbytes, data, 4);
        //将实际内容添加到bytes数组中
        Array.Copy(bodybytes, 0, data, 4, msgLen);
        serverSocket.Send(data);
    }

    void SocketQuit()
    {
        //关闭线程
        if (m_Thread != null)
        {
            m_Thread.Interrupt();
            m_Thread.Abort();
        }
        //最后关闭服务器
        if (serverSocket != null)
            serverSocket.Close();
        print("diconnect");
    }

    //程序退出则关闭连接
    void OnApplicationQuit()
    {
        SocketQuit();
    }
}
