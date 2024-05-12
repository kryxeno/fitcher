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
        GameEventSystem.instance.playerEvents.onCutsceneEnd += FinishThisStep;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.interactorEvents.onOpenCellarDoor -= OpenCellarDoor;
        GameEventSystem.instance.playerEvents.onCutsceneEnd -= FinishThisStep;
    }

    private void OpenCellarDoor()
    {
        string status = "You opened the cellar door.";
        ChangeState("", status);
        AudioManager.instance.PlayNarration("FindOut");

        GameEventSystem.instance.cutsceneEvents.PlayCutscene("WalkDownCellarCutscene", true);
    }

    private void FinishThisStep(string cutsceneName)
    {
        GameEventSystem.instance.playerEvents.UpdatePlayerPosition("Cellar");
        if (cutsceneName == "WalkDownCellarCutscene") FinishQuestStep();
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
