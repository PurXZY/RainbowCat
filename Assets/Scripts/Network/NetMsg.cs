using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMsg
{
    private int m_msgId;
    private byte[] m_msgData;

    public NetMsg(int msgId, byte[] msgData)
    {
        m_msgId = msgId;
        m_msgData = msgData;
    }

    public int GetMsgId()
    {
        return m_msgId;
    }

    public byte[] GetMsgData()
    {
        return m_msgData;
    }
}
