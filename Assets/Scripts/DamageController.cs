using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour
{
    public void AnimationEnd()
    {
        Destroy(gameObject);
    }

    public void SetDamageText(float damage)
    {
        GetComponent<Text>().text = damage.ToString();
    }
}
