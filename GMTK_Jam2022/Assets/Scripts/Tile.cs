using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3Int _coord = new Vector3Int();

    public bool _isCollapsable = true;

    public bool _canWalkOn ;
    

    public IEnumerator Collapse()
    {
        if(_isCollapsable)
        {
            TileManager.instance.tileDict.Remove(_coord);
            for (int i = 0; i < 20; i++)
            {
                transform.position = transform.position + new Vector3(0f,-2f/20,0f);
                yield return new WaitForSeconds(0.03f);
            }
            Destroy(gameObject);
        }
    }



}
