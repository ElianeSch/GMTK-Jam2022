using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : Tile
{
    public bool _isDown;
    public GameObject spike;
    public bool isMoving;
    public float timeToDown;
    public float timeToUp;
    public Animator animator;
    public Material allowedMaterial;

    private void Start()
    {
        spike = transform.GetChild(0).gameObject;
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Dice>() != null && !_isDown && !TileManager.instance.gameOver)
        {
            Face faceDown = other.gameObject.GetComponent<Dice>().GetFaceAt(5, out int index);
            if (faceDown.GetComponent<MeshRenderer>().sharedMaterial != allowedMaterial)
            {
                print("GameOver");
                TileManager.instance.gameOver = true;
            }
        }
    }

    public IEnumerator SpikeMovement()
    {
        while (isMoving)
        {
            _isDown = false;
            animator.SetBool("isDown", _isDown);
            yield return new WaitForSeconds(timeToDown);
            if (isMoving)
            {
                _isDown = true;
                animator.SetBool("isDown", _isDown);
                yield return new WaitForSeconds(timeToUp);
            }
        }
    }


}

