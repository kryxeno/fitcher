using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        Debug.Log("Key Picked Up");
        gameObject.SetActive(false);
        GameEventSystem.instance.interactorEvents.PickUpKey();
    }
}
