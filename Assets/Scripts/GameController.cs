using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public Tilemap tilemap;
    public GameObject tar;
    void Start()
    {
        Instance = this;
        var cell_b = tilemap.cellBounds;
        print(cell_b);
        var pos = Vector3Int.FloorToInt(tar.transform.position);
        
        var bounds = new BoundsInt(pos, new Vector3Int(3,3,1));
        print(bounds);
        foreach(var q in bounds.allPositionsWithin)
        {
            Vector3Int relativePos = new Vector3Int(q.x, q.y, q.z);
            bool has = false;
            if (tilemap.HasTile(relativePos))
                has = true;
            print(q + ": " + has);
        }
        print("--------------");
        var target = tilemap.GetTilesBlock(bounds);
        for (int index = 0; index < target.Length; index++)
        {
            print(target[index]);
        }
        print("-----a---------");
        var test = tilemap.GetTile(pos);
        print(test);
        var test1 = tilemap.GetTile(pos+new Vector3Int(1,0,0));
        print(test1);
        var test2 = tilemap.GetTile(pos + new Vector3Int(0, 1, 0));
        print(test2);
        var test3 = tilemap.GetTile(pos + new Vector3Int(0, 0, 1));
        print(test3);
        var test4 = tilemap.GetTile(new Vector3Int(-9, -5, 0));
        print(test4);

        var ret = GetCellsFromTilemap(tilemap);
        foreach(var l in ret)
        {
            print(l);
        }
        
    }

    void Update()
    {
        
    }

    List<Vector3> GetCellsFromTilemap(Tilemap tilemap)
    {
        List<Vector3> worldPosCells = new List<Vector3>();
        foreach (var boundInt in tilemap.cellBounds.allPositionsWithin)
        {
            //Get the local position of the cell
            Vector3Int relativePos = new Vector3Int(boundInt.x, boundInt.y, boundInt.z);
            //Add it to the List if the local pos exist in the Tile map
            if (tilemap.HasTile(relativePos))
            {
                //Convert to world space
                Vector3 worldPos = tilemap.CellToWorld(relativePos);
                worldPosCells.Add(worldPos);
            }
        }
        return worldPosCells;
    }
}
