using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCellarDoorArea : MonoBehaviour
{
    private bool hasFoundKey = false;

    private void Start()
    {
        PlayerPrefs.SetInt("CellarDoorFound", 0);
    }

    private void OnEnable()
    {
        GameEventSystem.instance.interactorEvents.onPickUpKey += CellarKeyFound;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.interactorEvents.onPickUpKey -= CellarKeyFound;
    }

    private void CellarKeyFound()
    {
        hasFoundKey = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Cellar door found!");
            PlayerPrefs.SetInt("CellarDoorFound", 1);
            if (!hasFoundKey) AudioManager.instance.PlayNarration("KeyToOpen");
            Destroy(gameObject);
        }
    }
}
