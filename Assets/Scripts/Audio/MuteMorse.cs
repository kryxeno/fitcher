using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMorse : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventSystem.instance.interactorEvents.onSolveMorse += MuteSound;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.interactorEvents.onSolveMorse -= MuteSound;
    }

    private void MuteSound()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
