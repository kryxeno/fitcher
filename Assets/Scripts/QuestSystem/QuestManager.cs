using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private bool loadQuestState = true;

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
        GameEventSystem.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.questEvents.onStartQuest -= StartQuest;
        GameEventSystem.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventSystem.instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventSystem.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;

    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
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
                StartQuest(quest.info.id);
                Debug.Log("Starting quest: " + quest.info.id);
                break;
            }
        }
    }

    public void StartQuest(string questId)
    {
        Quest quest = GetQuestById(questId);
        if (quest.state != QuestState.CAN_START)
        {
            Debug.LogError("Cannot start quest: " + questId);
            return;
        }
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
            AudioManager.instance.Play("QuestUpdated");
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.FINISHED);
            AudioManager.instance.Play("QuestCompleted");
        }
    }

    public void FinishQuest(string questId)
    {
        Quest quest = GetQuestById(questId);
        Debug.Log("Starting quest: " + questId);
    }

    public void QuestStepStateChange(string questId, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(questId);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(questId, quest.state);
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
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
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

    private void OnApplicationQuit()
    {
        foreach (Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }
    }

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();
            string serializedData = JsonUtility.ToJson(questData);
            PlayerPrefs.SetString(quest.info.id, serializedData);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving quest: " + quest.info.id + " " + e);
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;
        try
        {
            if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading quest: " + questInfo.id + " " + e);
        }
        return quest;
    }
}
