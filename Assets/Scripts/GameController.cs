using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public Image gameOverImg;

    void Start()
    {
        Instance = this;
        Application.targetFrameRate = 120;
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverImg.gameObject.SetActive(true);
    }
}
