using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractible
{
    public bool isLocked = true;
    public bool isCellarDoor;

    private void OnEnable()
    {
        GameEventSystem.instance.interactorEvents.onPickUpKey += UnlockCellarDoor;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.interactorEvents.onPickUpKey -= UnlockCellarDoor;
    }

    private void UnlockCellarDoor()
    {
        if (isCellarDoor)
        {
            isLocked = false;
        }
    }

    public void Interact()
    {
        if (isCellarDoor && !isLocked)
        {
            Debug.Log("Cellar door opened!");
            // GameEventSystem.instance.interactorEvents.EnterCellar();
            AudioManager.instance.Play("DoorOpening");
            gameObject.GetComponent<Animator>().SetTrigger("Open");
            return;
        }
        if (isLocked)
        {
            AudioManager.instance.Play("DoorLocked");
            return;
        }
        AudioManager.instance.Play("DoorOpening");
        gameObject.GetComponent<Animator>().SetTrigger("Open");
        Destroy(gameObject.GetComponent<OpenDoor>());
    }
}

