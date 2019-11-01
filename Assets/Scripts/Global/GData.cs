using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usercmd;
using System.IO;
using System.Text;

public class GData
{
    public static GData Instance { get; } = new GData();

    private Dictionary<uint, string> operationData = new Dictionary<uint, string>();

    public GData()
    {
        InitOperationData();
    }

    public Dictionary<uint, Vector2> PosIndexMap = new Dictionary<uint, Vector2> {
            {(uint)PosIndex.PosEleft, new Vector2(-7,-2) },
            {(uint)PosIndex.PosEcenter, new Vector2(-5.5f,-2f) },
            {(uint)PosIndex.PosEright, new Vector2(-4,-2) },
            {(uint)PosIndex.PosBleft, new Vector2(4,-2) },
            {(uint)PosIndex.PosBcenter, new Vector2(5.5f,-2) },
            {(uint)PosIndex.PosBright, new Vector2(7,-2) },
        };

    private void InitOperationData()
    {
        string jsonText = File.ReadAllText(Application.dataPath + "/JsonData/Operation.json", Encoding.UTF8);
        JSONObject j = new JSONObject(jsonText);
        foreach (var tmp in j.list)
        {
            uint id = (uint)tmp["id"].n;
            string name = tmp["name"].str;
            operationData[id] = name;
        }
    }

    public string GetOperationData(uint id)
    {
        if (operationData.ContainsKey(id))
            return operationData[id];
        return null;
    }
}