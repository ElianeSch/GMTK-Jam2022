using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public bool _isMoving = false;
    public Vector3Int _coord = new Vector3Int();
    public float _rollSpeed = 3f;
    public List<Face> listFaces;
    public Tile currentTile;




    private void Start()
    {
        _coord = TileManager.instance.PositionToCoord(transform.position);
        currentTile = TileManager.instance.GetTileFromCoord(_coord);

        for(int i = 0; i<6;i++)
        {
            ChangeFace(i, FaceManager.instance.allFacesPrefabs[i]);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeFace(5, FaceManager.instance.allFacesPrefabs[1]);
        }
            if (!_isMoving)
        {
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Rolling(new Vector3(0f, 0f, 1f));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Rolling(new Vector3(0f, 0f, -1f));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Rolling(new Vector3(-1f, 0f, 0f));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Rolling(new Vector3(1f, 0f, 0f));
            }
        }
    }

    private void Rolling(Vector3 dir)
    {
        if (CanDiceMoveHere(dir))
        {
            Vector3 anchor = transform.position + (new Vector3(0f, -1f, 0f) + dir) * 0.5f;
            Vector3 axis = Vector3.Cross(new Vector3(0f, 1f, 0f), dir);
            StartCoroutine(Roll(anchor, axis));
            _coord += Vector3Int.RoundToInt(dir);
            StartCoroutine(currentTile.Collapse());

            
        }
    }

    private IEnumerator Roll(Vector3 anchor, Vector3 axis)
    {
        _isMoving = true;
        for(int i = 0; i<90/_rollSpeed; i++ )
        {
            transform.RotateAround(anchor, axis, _rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        _isMoving = false;
        UpdatesFacesPos();
        currentTile = TileManager.instance.GetTileFromCoord(_coord);
        if (currentTile.GetComponent<FaceTile>() != null)
        {
            ChangeFace(5, currentTile.GetComponent<FaceTile>().facePrefab);
        }
    }
    private bool CanDiceMoveHere(Vector3 dir)
    {
        Vector3Int newCoord = _coord + Vector3Int.RoundToInt(dir);
        bool isThereATile = TileManager.instance.GetTileFromCoord(newCoord) != null;
        if(isThereATile)
        {
            return TileManager.instance.GetTileFromCoord(newCoord)._canWalkOn;
        }

        return false;

    }

    private void ChangeFace(int posOnDice, Face facePrefab)
    {
        Face oldFace = GetFaceAt(posOnDice);
        Face newFace = Instantiate(facePrefab, transform);
        newFace.transform.rotation= oldFace.transform.rotation;
        int index = listFaces.IndexOf(oldFace);
        Destroy(oldFace.gameObject);
        listFaces[index] = newFace;
    }

    public void UpdatesFacesPos()
    {
        foreach(Face face in listFaces)
        {
            face.UpdateFacePosOnDice();
        }
    }

    public Face GetFaceAt(int posOnDice)
    {
        for (int i = 0; i < 6; i++)
        {
            if (listFaces[i]._facePosOnDice == posOnDice)
            {
                return (listFaces[i]);
            }
        }
        return null;
    }
}
