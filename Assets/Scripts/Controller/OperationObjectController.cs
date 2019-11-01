using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationObjectController : MonoBehaviour
{
    [SerializeField] private Text m_name = null;

    public void Init(string name)
    {
        m_name.text = name;
    }
}
