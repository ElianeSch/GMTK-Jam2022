using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public Dictionary<Vector3Int, Tile> tileDict = new Dictionary<Vector3Int, Tile>();

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("erreur plus d'une instance de TileManager");
        }
        else 
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(Tile tile in GameObject.FindObjectsOfType<Tile>())
        {
            tileDict.Add(tile._coord, tile);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3Int PositionToCoord(Vector3 positon)
    {
        Vector3Int coord = new Vector3Int(Mathf.RoundToInt(positon.x - 0.5f), Mathf.RoundToInt(positon.y), Mathf.RoundToInt(positon.z - 0.5f));
        return coord;
    }

    public Tile GetTileFromCoord(Vector3Int coord)
    {
        if (tileDict.ContainsKey(coord))
        {
            return tileDict[coord];
        }
        else
        {
            return null;
        }
    }

}
