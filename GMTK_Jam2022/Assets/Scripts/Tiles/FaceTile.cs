using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTile : Tile
{
    public Face face;

    private void Start()
    {
        face = GetComponentInChildren<Face>();
    }
}
