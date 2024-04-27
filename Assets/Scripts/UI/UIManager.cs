using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject genericUI;
    public GameObject crosshairUI;
    public bool UIHidden = false;

    private void OnEnable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart += HideUI;
        GameEventSystem.instance.playerEvents.onCutsceneEnd += ShowUI;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart -= HideUI;
        GameEventSystem.instance.playerEvents.onCutsceneEnd -= ShowUI;
    }

    private void HideUI()
    {
        genericUI.SetActive(false);
        crosshairUI.SetActive(false);
        UIHidden = true;
    }

    private void ShowUI()
    {
        genericUI.SetActive(true);
        crosshairUI.SetActive(true);
        UIHidden = false;
    }
}
