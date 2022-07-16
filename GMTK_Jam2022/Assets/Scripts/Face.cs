using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Face : MonoBehaviour
{
    public int _facePosOnDice;
    public int _value;
    private TextMeshPro _textValue;


    private void Start()
    {
        _textValue = GetComponentInChildren<TextMeshPro>();
        _textValue.text = _value.ToString();
        UpdateFacePosOnDice();
    }

    public void UpdateFacePosOnDice()
    {
        _facePosOnDice = FaceManager.instance.listPosOnDice.IndexOf(Vector3Int.RoundToInt(transform.forward));
    }
}
