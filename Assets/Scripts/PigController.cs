﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public List<Vector2> patrolPos;
    int patrolIndex = 0;
    private bool isInPatrol = true;
    float speed = 2.0f;
    private Animator animator;
    public Vector2 curDir
    {
        private set;
        get;
    }
    public GameObject threatValueObject;
    public Transform threatValueParent;
    public GameObject threatValueUI
    {
        private set;
        get;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        threatValueUI = Instantiate(threatValueObject, pos, Quaternion.identity);
        threatValueUI.transform.SetParent(threatValueParent);
        threatValueUI.transform.localScale = new Vector3(1, 1, 1);
        threatValueUI.GetComponent<ThreatValueController>().pig = this.transform;
    }

    void FixedUpdate()
    {
        if (isInPatrol)
            patrol();
    }

    void patrol()
    {
        var targetPos = patrolPos[patrolIndex];
        var dirV = targetPos - (Vector2)transform.position;
        curDir = dirV.normalized;
        animator.SetFloat("speedX", curDir.x);
        animator.SetFloat("speedY", curDir.y);
        transform.Translate(curDir * speed * Time.deltaTime);

        if (dirV.magnitude < 0.1f)
        {
            changePatrolIndex();
        }
    }

    void changePatrolIndex()
    {
        patrolIndex = patrolIndex + 1 >= patrolPos.Count ? 0 : patrolIndex + 1;
    }

    public void stopPatrol()
    {
        isInPatrol = false;
    }

    public void resumePatrol()
    {
        isInPatrol = true;
    }
}
