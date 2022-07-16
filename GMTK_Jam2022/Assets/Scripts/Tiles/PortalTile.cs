using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTile : Tile
{
    public Material element;
    public Animator animator;

    private void Start()
    {
        _canWalkOn = false;
    }

    private void OnTriggerStay(Collider collision)
    {

        if (collision.gameObject.GetComponent<Dice>() != null)
        {
            Dice dice = collision.gameObject.GetComponent<Dice>();
            if (!dice._isMoving )
            {
                Face face = dice.GetFaceAt(4, out int index);
                print(face.GetComponent<MeshRenderer>().sharedMaterial.name);
                if (face.GetComponent<MeshRenderer>().sharedMaterial == element)
                {
                    print("oui");
                    _canWalkOn = true;
                    animator.SetTrigger("OpenPortal");
                } 
            }
        }
    }
}
