using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterCutscene : MonoBehaviour
{
    public string cutsceneName;

    private void OnEnable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneEnd += Hide;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneEnd -= Hide;
    }

    private void Hide(string cutscene)
    {
        if (cutscene == cutsceneName)
        {
            gameObject.SetActive(false);
        }
    }
}
