using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PickUpKeyStep : QuestStep, IInteractible
{
    private void Start()
    {
        UpdateState();
    }

    public void Interact()
    {
        Debug.Log("Key Picked Up");
        gameObject.SetActive(false);
        string status = "You picked up the key.";
        ChangeState("", status);
        GameEventSystem.instance.interactorEvents.UnlockDoor("CellarDoor");
        GameEventSystem.instance.interactorEvents.PickUpKey();
        FinishQuestStep();
    }

    private void UpdateState()
    {
        string status = "Find the thing making that music.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state to implement
        UpdateState();
    }
}