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

    }

    private void Update()
    {
            if (!_isMoving && !ViewManager.instance._isRotating)
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
            ChangeFace(5, currentTile.GetComponent<FaceTile>().face);
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

    private void ChangeFace(int posOnDice, Face face)
    {

        Face oldFace = GetFaceAt(posOnDice, out int index);
        if(face._value != -1)
        {
            oldFace._value = face._value;
        }
        if (face.has_material)
        {
            oldFace.GetComponent<MeshRenderer>().sharedMaterial = face.GetComponent<MeshRenderer>().sharedMaterial;
        }
        oldFace.UpdateFaceValue();
        listFaces[index] = oldFace;


    }

    public void UpdatesFacesPos()
    {
        foreach(Face face in listFaces)
        {
            face.UpdateFacePosOnDice();
        }
    }

    public Face GetFaceAt(int posOnDice , out int index)
    {
        index = 0;
        for (int i = 0; i < 6; i++)
        {
            if (listFaces[i]._facePosOnDice == posOnDice)
            {
                index = i;
                return (listFaces[i]);
            }
        }
        return null;
    }
}
