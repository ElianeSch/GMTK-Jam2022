using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private bool _isMoving = false;
    public Vector3Int _coord = new Vector3Int();
    public float _rollSpeed = 3f;
    private void Start()
    {
        _coord = TileManager.instance.PositionToCoord(transform.position);
    }

    private void Update()
    {
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
            _isMoving = true;
            Vector3 anchor = transform.position + (new Vector3(0f, -1f, 0f) + dir) * 0.5f;
            Vector3 axis = Vector3.Cross(new Vector3(0f, 1f, 0f), dir);
            StartCoroutine(Roll(anchor, axis));
            _coord += Vector3Int.FloorToInt(dir);
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
    }
    private bool CanDiceMoveHere(Vector3 dir)
    {
        Vector3Int newCoord = _coord + Vector3Int.FloorToInt(dir);

        return (TileManager.instance.GetTileFromCoord(newCoord) != null);

    }
}
