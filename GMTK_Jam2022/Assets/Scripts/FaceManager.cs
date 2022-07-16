using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceManager : MonoBehaviour
{
    public List<Vector3Int> listPosOnDice;
    public static FaceManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("erreur plus d'une instance de FaceManager");
        }
        else
        {
            instance = this;
        }
        
    }

}
