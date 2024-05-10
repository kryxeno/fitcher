using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestEvents
{
    public event Action<string> onStartQuest;

    public void StartQuest(string questId)
    {
        if (onStartQuest != null)
        {
            onStartQuest(questId);
        }
    }

    public event Action<string> onAdvanceQuest;

    public void AdvanceQuest(string questId)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(questId);
        }
    }

    public event Action<string> onFinishQuest;

    public void FinishQuest(string questId)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(questId);
        }
    }

    public event Action<Quest> onQuestStateChange;

    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null)
        {
            onQuestStateChange(quest);
        }
    }

    public event Action<string, int, QuestStepState> onQuestStepStateChange;

    public void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (onQuestStepStateChange != null)
        {
            onQuestStepStateChange(id, stepIndex, questStepState);
        }
    }

    public event Action onCellarPOIFound;
    public void CellarPOIFound()
    {
        if (onCellarPOIFound != null)
        {
            onCellarPOIFound();
        }
    }
}
