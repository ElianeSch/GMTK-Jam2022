using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public Grid grid;
    public GameObject currentTileMap;
    public Dice PlayerDice;
    public float _rotateSpeed;
    public bool _isRotating;
    public static ViewManager instance;
    public Vector3 pivot;
    public Camera mainCamera;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("erreur plus d'une instance de ViewManager");
        }
        else
        {
            instance = this;
        }
      

    }
    private void Start()
    {
        UpdatePivot();
    }



    private void Update()
    {
        if(!PlayerDice._isMoving && ! _isRotating)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(RotateGrid(-90f));
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(RotateGrid(90f));
            }
        }
        
    }

    private IEnumerator RotateGrid(float angle)
    {
        _isRotating = true;
        float rotateSpeed = _rotateSpeed;
        if (angle < 0) { rotateSpeed = -_rotateSpeed; }
        for (int i = 0; i < angle / rotateSpeed; i++)
        {
            grid.transform.RotateAround(pivot,new Vector3(0f, 1f, 0f), rotateSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        _isRotating = false;
        TileManager.instance.ReloadTileDictWhenRoatateGrid();
        ReloadDiceCoord();
    }
    private IEnumerator MoveCamera(Vector3 translation, float distance, float speed)
    {
        for (int i = 0; i < distance / speed; i++)
        {
            mainCamera.transform.SetPositionAndRotation(mainCamera.transform.position+translation * speed, mainCamera.transform.rotation);
            yield return new WaitForSeconds(0.01f);
        }
    }

        public void ReloadDiceCoord()
    {
        foreach (Dice dice in GameObject.FindObjectsOfType<Dice>())
        {
            dice._coord = TileManager.instance.PositionToCoord(dice.transform.position);
            dice.currentTile = TileManager.instance.GetTileFromCoord(dice._coord);
        }
    }

    public void UpdatePivot()
    {
        foreach (Transform transform in currentTileMap.transform)
        {
            if (transform.CompareTag("Pivot"))
            {
                if(pivot != null)
                {
                    Vector3 oldPivot = pivot;
                    pivot = transform.GetComponent<Tile>().transform.position;
                    Vector3 translation = pivot - oldPivot;
                    float distance = translation.magnitude;
                    StartCoroutine(MoveCamera(translation.normalized, distance, 0.5f));
                    
                }
                else
                {
                    pivot = transform.GetComponent<Tile>().transform.position;
                }
                break;



            }
        }
    }
    }
