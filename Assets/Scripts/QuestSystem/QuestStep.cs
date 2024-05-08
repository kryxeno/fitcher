using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    private int stepIndex;

    public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;
        if (questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }
    }

    protected void FinishQuestStep(bool destroy = true)
    {
        if (!isFinished)
        {
            isFinished = true;
            GameEventSystem.instance.questEvents.AdvanceQuest(questId);
            if (destroy) Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState, string newStatus)
    {
        GameEventSystem.instance.questEvents.QuestStepStateChange(questId, stepIndex, new QuestStepState(newState, newStatus));
    }

    protected abstract void SetQuestStepState(string state);

}
