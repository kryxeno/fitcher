using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem instance;

    public QuestEvents questEvents;
    public PlayerEvents playerEvents;
    public InteractorEvents interactorEvents;
    public CutsceneEvents cutsceneEvents;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        questEvents = new QuestEvents();
        playerEvents = new PlayerEvents();
        interactorEvents = new InteractorEvents();
        cutsceneEvents = new CutsceneEvents();
    }
}
