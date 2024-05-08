using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCutsceneCharacters : MonoBehaviour
{
    private GameObject[] cutsceneCharacters;

    private void Start()
    {
        // Assuming the GameObjects you want to store are direct children of the current GameObject
        int childCount = transform.childCount;
        cutsceneCharacters = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            cutsceneCharacters[i] = transform.GetChild(i).gameObject;
        }
    }

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
        AudioManager.instance.Play("Ambiance");

        foreach (GameObject character in cutsceneCharacters)
        {
            character.SetActive(false);
        }
    }
}
