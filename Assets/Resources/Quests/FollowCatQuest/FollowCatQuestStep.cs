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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string status = "You found the cat!";
            ChangeState("", status);
            FinishQuestStep();
        }
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
