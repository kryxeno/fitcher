using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour, IInteractible
{
    public string Interact()
    {
        Debug.Log("Key Picked Up");
        gameObject.SetActive(false);
        return gameObject.tag ?? "Object";
    }
}
