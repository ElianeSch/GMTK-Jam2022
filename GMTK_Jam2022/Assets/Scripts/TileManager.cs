using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public List<GameObject> listTileMap;
    public List<GameObject> listActiveTileMaps;
    public Dictionary<Vector3Int, Tile> tileDict = new Dictionary<Vector3Int, Tile>();
    public bool gameOver =  false;

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
        foreach(GameObject tileMap in listTileMap)
        {
            tileMap.SetActive(false);
        }
        listTileMap[0].SetActive(true);
        listTileMap[1].SetActive(true);
        listTileMap[2].SetActive(true);
        
        foreach (Tile tile in listTileMap[0].GetComponentsInChildren<Tile>())
        {
            tile._coord = PositionToCoord(tile.transform.position);
            tileDict.Add(tile._coord, tile);
        }
    }


    private void Start()
    {
        foreach (SpikeTile spike in GameObject.FindObjectsOfType<SpikeTile>())
        {
            StartCoroutine(spike.SpikeMovement());
        }
    }

    public Vector3Int PositionToCoord(Vector3 positon)
    {
        Vector3Int coord = new Vector3Int(Mathf.RoundToInt(positon.x - 0.5f), 0, Mathf.RoundToInt(positon.z - 0.5f));
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

    public void ReloadTileDictWhenRoatateGrid()
    {
        tileDict = new Dictionary<Vector3Int, Tile>();
        foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
        {
            if (tile.enabled)
            {
                tile._coord = PositionToCoord(tile.transform.position);
                tileDict.Add(tile._coord, tile);
            }
        }
    }


}
