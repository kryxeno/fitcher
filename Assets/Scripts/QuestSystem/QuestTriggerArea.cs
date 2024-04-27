using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(BoxCollider))]
public class QuestTriggerArea : MonoBehaviour
{
    [SerializeField] private QuestInfoSO questInfoForTrigger;
    private string questId;
    private QuestState currentQuestState;

    public enum Options
    {
        START,
        FINISH
    }

    [Header("Config")]
    public Options triggerType;

    private void Awake()
    {
        questId = questInfoForTrigger.id;
    }

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
        if (quest.info.id == questId)
        {
            currentQuestState = quest.state;
            Debug.Log("Quest state changed to: " + currentQuestState);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerType == Options.START && currentQuestState == QuestState.CAN_START)
            {
                GameEventSystem.instance.questEvents.StartQuest(questId);
            }
            else if (triggerType == Options.FINISH && currentQuestState == QuestState.CAN_FINISH)
            {
                GameEventSystem.instance.questEvents.FinishQuest(questId);
            }
        }
    }
}
