using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Face : MonoBehaviour
{
    public int _facePosOnDice;
    public int _value;
    private TextMeshPro _textValue;
    public bool has_material;


    private void Start()
    {
        UpdateFaceValue();
        UpdateFacePosOnDice();
    }

    public void UpdateFacePosOnDice()
    {
        _facePosOnDice = FaceManager.instance.listPosOnDice.IndexOf(Vector3Int.RoundToInt(transform.forward));
    }

    public void UpdateFaceValue()
    {
        _textValue = GetComponentInChildren<TextMeshPro>();
        if(_value != -1)
        {
            _textValue.text = _value.ToString();
        }
        else
        {
            _textValue.text = " ";
        }
    }
}
