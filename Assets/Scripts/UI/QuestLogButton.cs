using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class QuestLogButton : MonoBehaviour, ISelectHandler
{

    public Button button { get; private set; }
    private TextMeshProUGUI buttonText;
    public UnityAction onSelectAction;

    public void Initialize(string displayName, UnityAction selectAction)
    {
        this.button = GetComponent<Button>();
        this.buttonText = GetComponentInChildren<TextMeshProUGUI>();
        this.buttonText.text = displayName;
        this.onSelectAction = selectAction;
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }
}

