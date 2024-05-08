using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PickUpNoteStep : QuestStep, IInteractible
{
    private void Start()
    {
        UpdateState();
    }

    public void Interact()
    {
        Debug.Log("Note Picked Up");
        gameObject.SetActive(false);
        string status = "You picked up the missing page.";
        ChangeState("", status);
        GameEventSystem.instance.interactorEvents.ShowNote();
        FinishQuestStep();
    }

    private void UpdateState()
    {
        string status = "Find the location of the missing page.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state to implement
        UpdateState();
    }
}