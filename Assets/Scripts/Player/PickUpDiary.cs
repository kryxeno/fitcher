using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDiary : MonoBehaviour, IInteractible
{
    public string Interact()
    {
        Debug.Log("Diary Picked Up");
        gameObject.SetActive(false);
        return gameObject.tag ?? "Object";
    }
}
