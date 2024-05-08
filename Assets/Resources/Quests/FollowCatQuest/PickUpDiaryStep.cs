using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PickUpDiaryStep : QuestStep, IInteractible
{
    private void Start()
    {
        UpdateState();
    }

    public void Interact()
    {
        Debug.Log("Diary Picked Up");
        gameObject.SetActive(false);
        string status = "You picked up the diary";
        ChangeState("", status);
        GameEventSystem.instance.interactorEvents.ShowDiary();
        FinishQuestStep();
    }

    private void UpdateState()
    {
        string status = "Pick up the diary.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state to implement
        UpdateState();
    }
}