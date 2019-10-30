using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountInfoPanelController : MonoBehaviour
{
    [SerializeField] private GameObject accountText = null;
    [SerializeField] private GameObject playerIdtext = null;

    public void ShowMe()
    {
        accountText.GetComponent<Text>().text = "Account: " + G.Instance.accountStr;
        playerIdtext.GetComponent<Text>().text = "PlayerId: " + G.Instance.playerId;
        this.gameObject.SetActive(true);
    }
}
