using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField]
    GameObject posCircle = null;
    public List<Vector2> teamAPosInfo;
    public List<Vector2> teamBPosInfo;
    List<GameObject> teamAPosObjects = new List<GameObject>();
    List<GameObject> teamBPosObjects = new List<GameObject>();

    void Start()
    {
        Application.targetFrameRate = 60;
        InitAllPosCircle();
    }

    void Update()
    {
        
    }

    void InitAllPosCircle()
    {
        foreach(var pos in teamAPosInfo)
        {
            GameObject posCircleObject = Instantiate(posCircle, pos, Quaternion.identity);
            teamAPosObjects.Add(posCircleObject);
        }
        foreach (var pos in teamBPosInfo)
        {
            GameObject posCircleObject = Instantiate(posCircle, pos, Quaternion.identity);
            teamBPosObjects.Add(posCircleObject);
        }
    }
}
