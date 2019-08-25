using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreatValueController : MonoBehaviour
{
    public GameObject valueObject;
    public Transform pig;
    void Start()
    {
        SetValue(0);
    }
    
    public void SetValue(float value)
    {
        if (value >= 1.0f)
        {
            value = 1.0f;
            valueObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            valueObject.GetComponent<Image>().color = Color.yellow;
        }
        valueObject.transform.localScale = new Vector3(1, value, 1);
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(pig.position) + new Vector3(0,80,0);
    }
}
