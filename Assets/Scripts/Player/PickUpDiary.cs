using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDiary : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        Debug.Log("Diary Picked Up");
        gameObject.SetActive(false);
        GameEventSystem.instance.interactorEvents.ShowDiary();
    }

    private void OnDestroy()
    {
        GameEventSystem.instance.interactorEvents.ShowDiary();
    }
}
