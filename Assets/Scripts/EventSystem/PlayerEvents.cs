using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEvents
{
    public event Action onEnablePlayerMovement;

    public void EnablePlayerMovement()
    {
        if (onEnablePlayerMovement != null)
        {
            onEnablePlayerMovement();
        }
    }

    public event Action onDisablePlayerMovement;

    public void DisablePlayerMovement()
    {
        if (onDisablePlayerMovement != null)
        {
            onDisablePlayerMovement();
        }
    }

    public event Action onCutsceneStart;
    public void CutsceneStart()
    {
        if (onCutsceneStart != null)
        {
            onCutsceneStart();
        }
    }

    public event Action onCutsceneEnd;
    public void CutsceneEnd()
    {
        if (onCutsceneEnd != null)
        {
            onCutsceneEnd();
        }
    }
}
