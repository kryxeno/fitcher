using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutUpCat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventSystem.instance.playerEvents.ShutUpCat();
        }
    }
}
