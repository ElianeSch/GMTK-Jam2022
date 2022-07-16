using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : Tile
{
    public bool _isDown = false;
    public Animator animator;
    public GateTile gateTile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Dice>() != null)
        {
            _isDown = true;
            animator.SetTrigger("isDown");
            gateTile._isOpen = true;
            gateTile.animator.SetTrigger("OpenGate");
            gateTile._canWalkOn = true;
        }
    }

}
