using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTile : Tile
{
    public GameObject tileMap;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Dice>() != null)
        {
            print("Win");
            ViewManager.instance.currentTileMap = tileMap;
            ViewManager.instance.UpdatePivot();
        }
    }
}
