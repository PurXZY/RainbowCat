using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthController : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private GameObject healthUIObject = null;

    public void CreateHealthBar(uint entityPosIndex)
    {
        var h = Instantiate(healthUIObject, this.gameObject.transform);
        var c = h.GetComponent<HealthUIController>();
        c.SetOwner(entityPosIndex, rectTransform);
    }
}
