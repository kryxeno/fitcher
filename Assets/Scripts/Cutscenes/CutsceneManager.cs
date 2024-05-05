using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public List<PlayableDirector> cutscenes;
    public FirstPersonController playerController;
    public bool conditionMet
    { get; set; }

    void Start()
    {
        GameObject cutsceneParent = GameObject.Find("Cutscenes");
        foreach (Transform child in cutsceneParent.transform)
        {
            if (child.GetComponent<PlayableDirector>() != null)
            {
                cutscenes.Add(child.GetComponent<PlayableDirector>());
            }
        }
        PlayCutscene("IntroCutscene", true);
    }

    public void PlayCutscene(string cutsceneName, bool lockPlayerMovement)
    {
        foreach (PlayableDirector cutscene in cutscenes)
        {
            if (cutscene.name == cutsceneName)
            {
                Debug.Log("Playing Cutscene: " + cutsceneName);
                if (lockPlayerMovement) GameEventSystem.instance.playerEvents.CutsceneStart();
                cutscene.Play();
                cutscene.stopped += OnCutsceneFinished;
            }
        }
    }

    void OnCutsceneFinished(PlayableDirector aDirector)
    {
        GameEventSystem.instance.playerEvents.CutsceneEnd();
        aDirector.stopped -= OnCutsceneFinished;
    }

}
