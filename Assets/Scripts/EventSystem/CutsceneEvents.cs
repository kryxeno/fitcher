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

    public event Action<string, string> onShowSubtitles;
    public void ShowSubtitles(string subtitle, string narrator)
    {
        if (onShowSubtitles != null)
        {
            onShowSubtitles(subtitle, narrator);
        }
    }

    public event Action onClearSubtitles;
    public void ClearSubtitles()
    {
        if (onClearSubtitles != null)
        {
            onClearSubtitles();
        }
    }
}
