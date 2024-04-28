using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    public int currentQuestStepIndex;
    private QuestStepState[] questStepStates;

    public Quest(QuestInfoSO info)
    {
        this.info = info;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepPrefabs.Length];
        for (int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }

    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.info = questInfo;
        this.state = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

        if (this.questStepStates.Length != this.info.questStepPrefabs.Length)
        {
            Debug.LogError("Quest step states length does not match quest step prefabs length for quest: " + this.info.id + ". Please RESET the quest data.");
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogError("No more quest steps to instantiate. ID: " + info.id);
        }
        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if (stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
            questStepStates[stepIndex].status = questStepState.status;
        }
        else
        {
            Debug.LogError("Invalid step index: " + stepIndex + " for quest: " + info.id);
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }

    public string GetFullStatusText()
    {
        string fullStatusText = "";
        if (state == QuestState.REQUIREMENTS_NOT_MET)
        {
            fullStatusText = "You cannot start this quest yet.";
        }
        else if (state == QuestState.CAN_START)
        {
            fullStatusText = "You can start this quest now.";
        }
        else
        {
            for (int i = 0; i < currentQuestStepIndex; i++)
            {
                fullStatusText += "<s>" + questStepStates[i].status + "</s>\n";
            }
            if (CurrentStepExists())
            {
                fullStatusText += questStepStates[currentQuestStepIndex].status;
            }
            if (state == QuestState.CAN_FINISH)
            {
                fullStatusText += "Quest can be finished.";
            }
            else if (state == QuestState.FINISHED)
            {
                fullStatusText += "Quest has been completed!";
            }
        }
        return fullStatusText;
    }
}
