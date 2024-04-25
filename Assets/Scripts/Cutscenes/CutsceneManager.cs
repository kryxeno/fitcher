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
    }

    public void PlayCutscene(string cutsceneName, bool lockPlayerMovement)
    {
        foreach (PlayableDirector cutscene in cutscenes)
        {
            if (cutscene.name == cutsceneName)
            {
                Debug.Log("Playing Cutscene: " + cutsceneName);
                playerController.playerCanMove = !lockPlayerMovement;
                playerController.cameraCanMove = !lockPlayerMovement;
                playerController.crosshair = !lockPlayerMovement;
                playerController.arms.gameObject.SetActive(!lockPlayerMovement);
                cutscene.Play();
                cutscene.stopped += OnCutsceneFinished;
            }
        }
    }

    void OnCutsceneFinished(PlayableDirector aDirector)
    {
        playerController.playerCanMove = true;
        playerController.cameraCanMove = true;
        playerController.crosshair = true;
        playerController.arms.gameObject.SetActive(true);
        aDirector.stopped -= OnCutsceneFinished;
    }

}
