using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Collections;

public class OperationPanelController : MonoBehaviour
{
    [SerializeField] private GameObject operationObject = null;
    private Transform m_transform = null;

    private void Start()
    {
        m_transform = this.gameObject.transform;
    }

    public void ShowOperations(RepeatedField<uint> operationSet)
    {
        foreach (var op in operationSet)
        {
            var t = Instantiate(operationObject, m_transform);
            var c = t.GetComponent<OperationObjectController>();
            var name = GData.Instance.GetOperationData(op);
            c.Init(name);
        }
    }
}
