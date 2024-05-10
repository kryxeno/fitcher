using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        string currentText = "";
        float currentAlpha = 0f;

        if (!text) { return; }

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0f)
            {
                ScriptPlayable<SubtitleBehavior> inputPlayable = (ScriptPlayable<SubtitleBehavior>)playable.GetInput(i);

                SubtitleBehavior input = inputPlayable.GetBehaviour();
                currentText = input.subtitleText;
                currentAlpha = inputWeight;
            }
        }

        text.text = currentText + ":";
        if (currentText == "John")
        {
            text.color = GetNarratorColor("John", currentAlpha);
        }
        else if (currentText == "Emilia")
        {
            text.color = GetNarratorColor("Emilia", currentAlpha);
        }
        else if (currentText == "Narrator")
        {
            text.color = GetNarratorColor("Narrator", currentAlpha);
        }
        else
        {
            text.text = currentText;
            text.color = new Color(1, 1, 1, currentAlpha);
        }
    }

    public Color GetNarratorColor(string narrator, float alpha = 1f)
    {
        switch (narrator)
        {
            case "Emilia":
                return new Color(0.8f, 0.2f, 0.2f, alpha);
            case "John":
                return new Color(0.2f, 0.2f, 0.8f, alpha);
            case "Narrator":
                return new Color(0.2f, 0.8f, 0.2f, alpha);
            default:
                return new Color(0.8f, 0.8f, 0.8f, alpha);
        }
    }
}
