using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GoToCellarDoorStep : QuestStep
{
    private void Start()
    {
        UpdateState();

        if (PlayerPrefs.GetInt("CellarDoorFound") == 1)
        {
            FinishQuestStep();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string status = "Figure out what the key is for.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state to implement
        UpdateState();
    }
}
