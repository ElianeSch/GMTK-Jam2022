using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : Tile
{
    public int _value;
    private TextMeshPro _textValue;


    private void Start()
    {
        _canWalkOn = false;
        _textValue = GetComponentInChildren<TextMeshPro>();
        _textValue.text = _value.ToString();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<Dice>() !=null)
        {
            Dice dice = collision.gameObject.GetComponent<Dice>();
            if (!dice._isMoving && dice.GetFaceAt(4)._value >= _value)
            {
                _canWalkOn = true;
            }
        }
    }
}
