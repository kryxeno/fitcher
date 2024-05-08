using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CutsceneEvents
{
    public event Action<string, bool> onPlayCutscene;

    public void PlayCutscene(string cutsceneName, bool lockPlayerMovement = true)
    {
        if (onPlayCutscene != null)
        {
            onPlayCutscene(cutsceneName, lockPlayerMovement);
        }
    }
}
