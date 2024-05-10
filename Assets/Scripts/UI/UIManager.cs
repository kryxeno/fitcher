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
        ClearSubtitles();
    }

    private void OnEnable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart += HideUI;
        GameEventSystem.instance.playerEvents.onCutsceneEnd += ShowUI;
        GameEventSystem.instance.interactorEvents.onPickUpKey += PickUpKey;
        GameEventSystem.instance.cutsceneEvents.onShowSubtitles += ShowSubtitles;
        GameEventSystem.instance.cutsceneEvents.onClearSubtitles += ClearSubtitles;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart -= HideUI;
        GameEventSystem.instance.playerEvents.onCutsceneEnd -= ShowUI;
        GameEventSystem.instance.interactorEvents.onPickUpKey -= PickUpKey;
        GameEventSystem.instance.cutsceneEvents.onShowSubtitles -= ShowSubtitles;
        GameEventSystem.instance.cutsceneEvents.onClearSubtitles -= ClearSubtitles;
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
            TextMeshProUGUI editText = child.gameObject.GetComponent<TextMeshProUGUI>();
            editText.text = "";
            editText.color = new Color(1, 1, 1, 0);

        }
    }

    private void ShowSubtitles(string subtitle, string narrator)
    {
        Debug.Log("Showing subtitles: " + subtitle + " by " + narrator);
        TextMeshProUGUI narratorText = subtitleUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subtitleText = subtitleUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        narratorText.text = narrator;
        narratorText.color = GetNarratorColor(narrator);
        subtitleText.text = subtitle;
        subtitleText.color = new Color(1, 1, 1, 1);
    }

    private void PickUpKey()
    {
        keyImage.color = new Color(1, 1, 1, 1);
    }

    public Color GetNarratorColor(string narrator)
    {
        switch (narrator)
        {
            case "Emilia":
                return new Color(0.8f, 0.2f, 0.2f);
            case "John":
                return new Color(0.2f, 0.2f, 0.8f);
            case "Narrator":
                return new Color(0.2f, 0.8f, 0.2f);
            default:
                return new Color(0.8f, 0.8f, 0.8f);
        }
    }
}
