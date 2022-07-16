using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTile : Tile
{
    public Face facePrefab;

    private void Start()
    {
        Face oldFace = GetComponentInChildren<Face>();
        Face newFace = Instantiate(facePrefab, transform);
        newFace.transform.position = oldFace.transform.position;
        newFace.transform.rotation = oldFace.transform.rotation;
        newFace._facePosOnDice = 5;
        Destroy(oldFace.gameObject);
    }
}
