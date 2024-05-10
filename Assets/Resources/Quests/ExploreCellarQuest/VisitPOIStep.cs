using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitPOIStep : QuestStep
{
    private int visitedPOIs = 0;


    private void Start()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventSystem.instance.questEvents.onCellarPOIFound += POIFound;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.questEvents.onCellarPOIFound -= POIFound;
    }

    private void POIFound()
    {
        visitedPOIs++;
        CheckPOIsVisited();
    }

    private void CheckPOIsVisited()
    {
        if (visitedPOIs == 3)
        {
            string status = "You have explored the entire cellar. This man is crazy!";
            ChangeState("", status);
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string status = "Figure out what he has hidden in the cellar.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state to implement
        UpdateState();
    }
}