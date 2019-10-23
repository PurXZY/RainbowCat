using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMgr
{
    private Dictionary<string, GameObject> entities;

    public static EntityMgr Instance { get; private set; }

    public void RegisterEntity(string id, GameObject target)
    {
        entities.Add(id, target);
    }

    public GameObject GetEntityById(string id)
    {
        return entities[id];
    }
}
