using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExitCellarStep : QuestStep
{
    private void Start()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneEnd += FinishThisStep;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneEnd -= FinishThisStep;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventSystem.instance.cutsceneEvents.PlayCutscene("ExitCellar", true);
        }
    }

    private void FinishThisStep(string cutsceneName)
    {
        if (cutsceneName == "ExitCellar") FinishQuestStep();
    }

    private void UpdateState()
    {
        string status = "Get out of the cellar before he comes back!";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state to implement
        UpdateState();
    }
}
