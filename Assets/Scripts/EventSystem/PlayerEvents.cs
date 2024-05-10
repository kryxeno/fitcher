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

    public event Action<string> onCutsceneEnd;
    public void CutsceneEnd(string cutsceneName = "")
    {
        if (onCutsceneEnd != null)
        {
            onCutsceneEnd(cutsceneName);
        }
    }

    public event Action<int> onMoveCat;

    public void MoveCat(int transformIndex)
    {
        if (onMoveCat != null)
        {
            onMoveCat(transformIndex);
        }
    }

    public event Action onShutUpCat;

    public void ShutUpCat()
    {
        if (onShutUpCat != null)
        {
            onShutUpCat();
        }
    }
}
