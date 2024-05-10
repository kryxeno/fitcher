using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractible
{
    public bool isLocked = true;
    public string doorName;


    private void OnEnable()
    {
        GameEventSystem.instance.interactorEvents.onUnlockDoor += UnlockDoor;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.interactorEvents.onUnlockDoor -= UnlockDoor;
    }

    private void UnlockDoor(string _doorName)
    {
        if (_doorName == doorName) isLocked = false;
    }

    public void Interact()
    {
        if (doorName == "CellarDoor" && !isLocked)
        {
            Debug.Log("Cellar door opened!");
            GameEventSystem.instance.interactorEvents.OpenCellarDoor();
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

