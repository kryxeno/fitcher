using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartMenu : MonoBehaviour
{
    public CinemachineVirtualCamera startCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayStartingCutscene()
    {
        GameEventSystem.instance.cutsceneEvents.PlayCutscene("IntroCutscene", true);
        Cursor.lockState = CursorLockMode.Locked;
        startCamera.Priority = 0;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
