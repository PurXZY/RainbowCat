using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnInfoPanelController : MonoBehaviour
{
    [SerializeField] private Text turnInfoText;

    public void SetTurnInfoText(uint turn)
    {
        this.gameObject.SetActive(true);
        turnInfoText.text = "回合: " + turn;
    }
}
