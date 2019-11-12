using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Collections;

public class OperationPanelController : MonoBehaviour
{
    [SerializeField] private GameObject operationObject = null;
    private Transform m_transform = null;
    private Dictionary<uint, OperationObjectController> m_operationSet = new Dictionary<uint, OperationObjectController>();


    private void Start()
    {
        m_transform = this.gameObject.transform;
    }

    public void ShowOperations(RepeatedField<uint> operationSet)
    {
        m_operationSet.Clear();
        foreach (var op in operationSet)
        {
            var t = Instantiate(operationObject, m_transform);
            var c = t.GetComponent<OperationObjectController>();
            c.Init(op);
            m_operationSet.Add(op, c);
        }
    }

    public void HideOthers(uint me, bool isShow=false)
    {
        foreach(var c in m_operationSet.Values)
        {
            if (c.operationId == me)
                continue;
            if (isShow)
                c.ShowMe();
            else
                c.HideMe();
        }
    }
}
