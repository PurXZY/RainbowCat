using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public static UIMgr Instance;

    private void Awake()
    {
        Instance = this;
    }


    [SerializeField] private Text m_TurnInfoText = null;

    public void SetTurnInfoText(int turn)
    {
        m_TurnInfoText.text = "回合: " + turn;
    }
}
