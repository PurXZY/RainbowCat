using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject bubble;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            ShowBubble();
        }
    }
    
    private void ShowBubble()
    {
        bubble.SetActive(true);
        // bubble.GetComponent<Animation>().Play("bubble_idle");
    }
}
