using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject genericUI;
    public GameObject crosshairUI;
    public GameObject subtitleUI;
    public GameObject skipText;
    public RawImage keyImage;
    public bool UIHidden = false;

    public void Start()
    {
        HideUI();
    }

    private void OnEnable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart += HideUI;
        GameEventSystem.instance.playerEvents.onCutsceneEnd += ShowUI;
        GameEventSystem.instance.interactorEvents.onPickUpKey += PickUpKey;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart -= HideUI;
        GameEventSystem.instance.playerEvents.onCutsceneEnd -= ShowUI;
        GameEventSystem.instance.interactorEvents.onPickUpKey -= PickUpKey;
    }

    private void HideUI()
    {
        genericUI.SetActive(false);
        crosshairUI.SetActive(false);
        UIHidden = true;
    }

    private void ShowUI(string cutsceneName)
    {
        genericUI.SetActive(true);
        crosshairUI.SetActive(true);
        ClearSubtitles();
        UIHidden = false;
    }

    private void ClearSubtitles()
    {
        skipText.SetActive(false);
        foreach (Transform child in subtitleUI.transform)
        {
            child.gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    private void PickUpKey()
    {
        keyImage.color = new Color(1, 1, 1, 1);
    }
}
