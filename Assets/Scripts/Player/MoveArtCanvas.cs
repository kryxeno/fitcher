using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArtCanvas : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Move");
        AudioManager.instance.Play("Dragging");
        Destroy(gameObject.GetComponent<MoveArtCanvas>());
    }
}

