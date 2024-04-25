using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpNote : MonoBehaviour, IInteractible
{
    public string noteName;
    public string noteText;

    public string Interact()
    {
        Debug.Log("Note Picked Up");
        gameObject.SetActive(false);
        return gameObject.tag ?? "Object";
    }


}
