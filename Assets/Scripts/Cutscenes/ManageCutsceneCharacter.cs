using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCutscene : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart += CutsceneStart;
        GameEventSystem.instance.playerEvents.onCutsceneEnd += CutsceneEnd;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart -= CutsceneStart;
        GameEventSystem.instance.playerEvents.onCutsceneEnd -= CutsceneEnd;
    }

    private void CutsceneStart()
    {
        gameObject.SetActive(true);
    }

    private void CutsceneEnd()
    {
        gameObject.SetActive(false);
    }
}
