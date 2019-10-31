using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usercmd;

public class GData
{
    public static GData Instance { get; } = new GData();

    public Dictionary<uint, Vector2> PosIndexMap = new Dictionary<uint, Vector2> {
            {(uint)PosIndex.PosEleft, new Vector2(-7,-2) },
            {(uint)PosIndex.PosEcenter, new Vector2(-5.5f,-2f) },
            {(uint)PosIndex.PosEright, new Vector2(-4,-2) },
            {(uint)PosIndex.PosBleft, new Vector2(4,-2) },
            {(uint)PosIndex.PosBcenter, new Vector2(5.5f,-2) },
            {(uint)PosIndex.PosBright, new Vector2(7,-2) },
        };
}
