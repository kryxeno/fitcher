using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameObject questLogPanel;
    [SerializeField] private QuestLogScrollingList scrollingList;
    [SerializeField] private TextMeshProUGUI questDisplayNameText;
    [SerializeField] private TextMeshProUGUI questStatusText;
    [SerializeField] private TextMeshProUGUI questRequirementsText;

    [SerializeField] private TextMeshProUGUI currentQuestStepText;


    private Button firstSelectedButton;

    private void OnEnable()
    {
        GameEventSystem.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.state != QuestState.IN_PROGRESS && quest.state != QuestState.FINISHED) return;

        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () => SetQuestLogInfo(quest));

        // Get the index of the new button
        int newIndex = scrollingList.transform.childCount - 1;

        // Move existing buttons down by one position
        for (int i = scrollingList.transform.childCount - 2; i >= 0; i--)
        {
            scrollingList.transform.GetChild(i).SetSiblingIndex(i + 1);
        }

        // Place the new button at the top of the list
        questLogButton.transform.SetSiblingIndex(0);

        firstSelectedButton = questLogButton.button;

        questLogButton.SetState(quest.state);
        currentQuestStepText.text = quest.GetCurrentQuestStepText();
    }



    private void SetQuestLogInfo(Quest quest)
    {
        questDisplayNameText.text = quest.info.displayName;

        questStatusText.text = quest.GetFullStatusText();

        questRequirementsText.text = "";
        foreach (QuestInfoSO questPrerequisite in quest.info.questPrerequisites)
        {
            questRequirementsText.text += questPrerequisite.displayName + "\n";
        }
    }

    private void QuestLogTogglePressed()
    {
        if (UIManager.UIHidden && !questLogPanel.activeInHierarchy) return;

        if (questLogPanel.activeInHierarchy)
        {
            questLogPanel.SetActive(false);
            GameEventSystem.instance.playerEvents.EnablePlayerMovement();
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            questLogPanel.SetActive(true);
            GameEventSystem.instance.playerEvents.DisablePlayerMovement();
            if (firstSelectedButton != null) firstSelectedButton.Select();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            QuestLogTogglePressed();
        }
    }

}
