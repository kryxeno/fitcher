using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCellarDoorArea : MonoBehaviour
{

    private void Start()
    {
        PlayerPrefs.SetInt("CellarDoorFound", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Cellar door found!");
            PlayerPrefs.SetInt("CellarDoorFound", 1);
            Destroy(gameObject);
        }
    }
}
