using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCutsceneCharacters : MonoBehaviour
{
    public GameObject[] cutsceneCharacters;

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
        foreach (GameObject character in cutsceneCharacters)
        {
            character.SetActive(true);
        }
    }

    private void CutsceneEnd()
    {
        foreach (GameObject character in cutsceneCharacters)
        {
            character.SetActive(false);
        }
    }
}
