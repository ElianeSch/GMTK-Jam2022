using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3Int _coord = new Vector3Int();

    private void Start()
    {
        _coord = TileManager.instance.PositionToCoord(transform.position); 
    }
    
  


}
