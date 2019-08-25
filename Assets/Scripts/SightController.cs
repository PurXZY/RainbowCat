using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{
    public float sightRadius = 3.0f;
    public float sightAngle = 60;
    public int inSightTime = 3;
    private CircleCollider2D sightCollider;
    private List<GameObject> playerNearby = new List<GameObject>();
    private Dictionary<GameObject, long> playerInSight = new Dictionary<GameObject, long>();

    private void Start()
    {
        sightCollider = gameObject.AddComponent<CircleCollider2D>();
        sightCollider.isTrigger = true;
        sightCollider.offset = Vector2.zero;
        sightCollider.radius = sightRadius;
    }

    private void Update()
    {
        DrawTriangle(transform.position, sightAngle, sightRadius, GetComponent<PigController>().curDir);
        CheckPlayerIsInSight();
    }

    private LineRenderer GetLineRenderer()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        return lr;
    }

    private void DrawTriangle(Vector3 startPos, float angle, float radius, Vector2 curDir)
    {
        LineRenderer lr = GetLineRenderer();
        int pointAmount = 50;
        float eachAngle = angle / pointAmount;

        lr.positionCount = pointAmount;
        startPos = startPos + new Vector3(0, 0, -1);
        lr.SetPosition(0, startPos);
        for (int i = 1; i < pointAmount-1; ++i)
        {
            Vector3 pos = startPos + Quaternion.Euler(0f, 0f, -angle/2 + eachAngle * (i - 1)) * curDir * radius;
            lr.SetPosition(i, pos);
        }
        lr.SetPosition(pointAmount-1, startPos);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            playerNearby.Add(coll.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            playerNearby.Remove(coll.gameObject);
            playerInSight.Remove(coll.gameObject);
        }
    }

    private void CheckPlayerIsInSight()
    {
        System.TimeSpan st = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0);
        long nowTime = System.Convert.ToInt64(st.TotalSeconds);

        List<GameObject> leaveSightPlayes = new List<GameObject>(playerNearby);
        foreach (var player in playerNearby)
        {
            
            Vector2 sightDir = player.transform.position - transform.position;
            float angle = Vector2.Angle(sightDir, GetComponent<PigController>().curDir);
            if (angle < sightAngle / 2)
            {
                leaveSightPlayes.Remove(player);
                if (!playerInSight.ContainsKey(player))
                    playerInSight[player] = nowTime;
                GetComponent<PigController>().stopPatrol();
            }
        }

        foreach (var player in leaveSightPlayes)
        {
            playerInSight.Remove(player);
        }


        foreach (var time in playerInSight.Values)
        {
            float scale = (float)(nowTime - time) / (float)inSightTime;
            GetComponent<PigController>().threatValueUI.GetComponent<ThreatValueController>().SetValue(scale);
            if (nowTime - time >= inSightTime)
            {
                // GameController.Instance.GameOver();
            }
        }

        if (playerInSight.Count == 0)
        {
            GetComponent<PigController>().resumePatrol();
        }
    }
}