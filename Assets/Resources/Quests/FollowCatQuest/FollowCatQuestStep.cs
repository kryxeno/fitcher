using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FollowCatQuestStep : QuestStep
{
    private void Start()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneEnd += FinishStep;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneEnd -= FinishStep;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string status = "You found the cat!";
            ChangeState("", status);
            GameEventSystem.instance.playerEvents.MoveCat(1);

            GameEventSystem.instance.cutsceneEvents.PlayCutscene("StudyCutscene", true);
        }
    }

    private void FinishStep(string questStep)
    {
        if (questStep == "StudyCutscene") FinishQuestStep();
    }

    private void UpdateState()
    {
        string status = "Find where the cat went.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state to implement
        UpdateState();
    }
}
