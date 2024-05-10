using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class NarratorSound : Sound
{
    public enum NarratorType { Emilia, John, Narrator };

    [Header("Narration")]
    public NarratorType narrator;
    public string subtitle;
}
