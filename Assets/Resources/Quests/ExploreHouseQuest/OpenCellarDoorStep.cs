using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCellarDoorStep : QuestStep
{
    private void Start()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventSystem.instance.interactorEvents.onOpenCellarDoor += OpenCellarDoor;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.interactorEvents.onOpenCellarDoor -= OpenCellarDoor;
    }

    private void OpenCellarDoor()
    {
        string status = "You opened the cellar door.";
        ChangeState("", status);
        FinishQuestStep();
    }

    private void UpdateState()
    {
        string status = "Open the cellar door.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state to implement
        UpdateState();
    }
}
