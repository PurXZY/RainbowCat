using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class SightController : MonoBehaviour
{
    void Update()
    {
        DrawTriangle(transform, transform.position, 60, 3, GetComponent<PigController>().curDir);
    }

    private static LineRenderer GetLineRenderer(Transform t)
    {
        LineRenderer lr = t.GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = t.gameObject.AddComponent<LineRenderer>();
        }
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        return lr;
    }

    public static void DrawTriangle(Transform t, Vector3 startPos, float angle, float radius, Vector2 curDir)
    {
        LineRenderer lr = GetLineRenderer(t);
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
}