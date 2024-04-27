using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventSystem.instance.questEvents.onStartQuest += StartQuest;
        GameEventSystem.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventSystem.instance.questEvents.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.questEvents.onStartQuest -= StartQuest;
        GameEventSystem.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventSystem.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            GameEventSystem.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventSystem.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool requirementsMet = true;
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                requirementsMet = false;
            }
        }
        return requirementsMet;
    }

    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
                break;
            }
        }
    }

    public void StartQuest(string questId)
    {
        Quest quest = GetQuestById(questId);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }

    public void AdvanceQuest(string questId)
    {
        Quest quest = GetQuestById(questId);
        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    public void FinishQuest(string questId)
    {
        Quest quest = GetQuestById(questId);
        Debug.Log("Starting quest: " + questId);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        // Loads all QuestInfoSOs from the Resources/Quests folder
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        // Create the questmap
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogError("Duplicate quest id found: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string questId)
    {
        Quest quest = questMap[questId];
        if (quest == null)
        {
            Debug.LogError("Quest not found in quest map: " + questId);
        }
        return quest;
    }
}
