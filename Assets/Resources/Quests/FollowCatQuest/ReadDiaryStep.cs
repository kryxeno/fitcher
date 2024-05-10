using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadDiaryStep : QuestStep
{
    private int[] pagesRead = new int[3];


    private void Start()
    {
        UpdateState();
        pagesRead[0] = 1;
        // AudioManager.instance.PlayNarration("DiaryPage1");
    }

    private void OnEnable()
    {
        GameEventSystem.instance.interactorEvents.onPageRead += PageRead;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.interactorEvents.onPageRead -= PageRead;
    }

    private void PageRead(int page)
    {
        // if (pagesRead[page] != 1) AudioManager.instance.PlayNarration("DiaryPage" + (page + 1));
        pagesRead[page] = 1;
        CheckPagesRead();
    }

    private void CheckPagesRead()
    {
        bool allPagesRead = true;
        foreach (int page in pagesRead)
        {
            if (page == 0)
            {
                allPagesRead = false;
                break;
            }
        }

        if (allPagesRead)
        {
            string status = "You read the diary. A page seems to be ripped out.";
            ChangeState("", status);
            GameEventSystem.instance.playerEvents.MoveCat(2);
            GameEventSystem.instance.interactorEvents.UnlockDoor("ArtroomDoor");
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string status = "Read the diary.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // No state to implement
        UpdateState();
    }
}