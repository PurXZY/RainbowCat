using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;

public class NetworkConnection
{
    private Socket m_TcpSocket;
    private IPEndPoint m_ServerIPEndPoint;

    private Thread m_recvThread;
    private Thread m_sendThread;
    private RingBuffer m_SendBuffer = new RingBuffer(64 * 1024);

    public void InitConnection()
    {
        if (m_TcpSocket != null && m_TcpSocket.Connected)
            return;
        m_ServerIPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
        m_TcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_TcpSocket.NoDelay = true;
        m_TcpSocket.BeginConnect(m_ServerIPEndPoint, AsyncConnectCallback, m_TcpSocket);
    }

    public void CloseConnection()
    {
        if (m_TcpSocket != null)
        {
            m_TcpSocket.Close();
            NetworkMgr.Instance.isConnectedToServer = false;
        }
            
    }

    public void AsyncConnectCallback(IAsyncResult result)
    {
        try
        {
            Socket client = (Socket)result.AsyncState;
            client.EndConnect(result);
            Debug.Log("connect server: " + client.Connected);
            m_recvThread = new Thread(ReceiveDataLoop);
            m_recvThread.Start();
            m_sendThread = new Thread(SendDataLoop);
            m_sendThread.Start();
            NetworkMgr.Instance.isConnectedToServer = true;
        }
        catch (Exception e)
        {
            Debug.LogError("AsyncConnectCallback Exception: " + e.Message);
        }
    }

    private void ReceiveDataLoop()
    {
        byte[] recvBuff = new byte[64 * 1024];
        int hasRecvDataLen = 0;
        int dataLen = 0;
        int msgLen = 0;
        int needSize = 4;
        while (m_TcpSocket.Connected)
        {
            if (m_TcpSocket.Poll(10 * 1000, SelectMode.SelectRead))
            {
                dataLen = m_TcpSocket.Receive(recvBuff, hasRecvDataLen, needSize, SocketFlags.None);
                if (dataLen == 0)
                {
                    m_TcpSocket.Close();
                    break;
                }
                hasRecvDataLen += dataLen;
                if (hasRecvDataLen < 4)
                    continue;
                msgLen = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(recvBuff, 0));
                if (hasRecvDataLen < msgLen + 4)
                {
                    needSize = msgLen + 4 - hasRecvDataLen;
                    continue;
                }
                ParseData(recvBuff, hasRecvDataLen);
                Array.Clear(recvBuff, 0, hasRecvDataLen);
                needSize = 4;
                hasRecvDataLen = 0;
            }
        }
        Debug.Log("ReceiveDataLoop Exit...");
        CloseConnection();
    }

    private void SendDataLoop()
    {
        byte[] tmpBuf = new byte[64 * 1024];
        int tmpBufSize = 0;
        int dataLen = 0;
        int hasSendDataLen = 0;

        while (m_TcpSocket.Connected)
        {
            if (m_SendBuffer.DataCount == 0 && hasSendDataLen == tmpBufSize)
            {
                Thread.Sleep(10);
                continue;
            }
            else if (hasSendDataLen < tmpBufSize)
            {

            }
            else
            {
                lock (m_SendBuffer)
                {
                    tmpBufSize = m_SendBuffer.DataCount;
                    m_SendBuffer.ReadBuffer(tmpBuf, 0, m_SendBuffer.DataCount);
                    m_SendBuffer.Clear(m_SendBuffer.DataCount);
                }
            }

            if (m_TcpSocket.Poll(10 * 1000, SelectMode.SelectWrite))
            {
                dataLen = m_TcpSocket.Send(tmpBuf, hasSendDataLen, tmpBufSize, SocketFlags.None);
                hasSendDataLen += dataLen;
                if (hasSendDataLen == tmpBufSize)
                {
                    Array.Clear(tmpBuf, 0, tmpBufSize);
                    tmpBufSize = 0;
                    hasSendDataLen = 0;
                }
            }
        }
        Debug.Log("SendDataLoop Exit...");
        CloseConnection();
    }

    public void SendData(UInt16 msgId, byte[] data)
    {
        int dataLen = data.Length;
        byte[] sendData = new byte[6 + dataLen];
        byte[] lenbytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataLen + 2));
        byte[] idbytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)msgId));

        Array.Copy(lenbytes, 0, sendData, 0, 4);
        Array.Copy(idbytes, 0, sendData, 4, 2);
        Array.Copy(data, 0, sendData, 6, dataLen);

        lock (m_SendBuffer)
        {
            m_SendBuffer.WriteBuffer(sendData);
        }
    }

    private void ParseData(byte[] data, int dataLen)
    {
        int msgId = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(data, 4));
        int bodyLen = dataLen - 6;
        byte[] msgBody = new byte[bodyLen];
        Buffer.BlockCopy(data, 6, msgBody, 0, bodyLen);
        var netMsg = new NetMsg(msgId, msgBody);
        lock(NetworkMgr.Instance.msgQueue)
        {
            NetworkMgr.Instance.msgQueue.Enqueue(netMsg);
        }
    }
}
