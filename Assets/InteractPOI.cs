using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InteractPOI : MonoBehaviour, IInteractible
{
    public string narrationName;

    public GameObject[] relatedPOI;

    public void Interact()
    {
        AudioManager.instance.PlayNarration(narrationName);
        gameObject.GetComponent<BoxCollider>().enabled = false;

        if (gameObject.CompareTag("CellarPOI")) GameEventSystem.instance.questEvents.CellarPOIFound();

        if (relatedPOI != null)
        {
            foreach (GameObject poi in relatedPOI)
            {
                poi.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
