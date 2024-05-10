using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public NarratorSound[] narratorSounds;

    public static AudioManager instance;

    [Header("Narration")]
    public bool forceNarratorVolume;
    public float narratorVolume = 0.8f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixerGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (NarratorSound s in narratorSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixerGroup;

            if (forceNarratorVolume) s.source.volume = narratorVolume;
            else s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    private NarratorSound lastNarrationSound;

    public void PlayNarration(string name)
    {
        NarratorSound s = System.Array.Find(narratorSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (lastNarrationSound != null && lastNarrationSound.source.isPlaying)
        {
            lastNarrationSound.source.Stop();
        }

        s.source.Play();

        lastNarrationSound = s;

        if (s.subtitle == null || s.narrator == null) return;
        GameEventSystem.instance.cutsceneEvents.ShowSubtitles(s.subtitle, s.narrator.ToString());
        StartCoroutine(WaitForSoundToFinish(s.source.clip.length, s));
    }

    private System.Collections.IEnumerator WaitForSoundToFinish(float soundDuration, NarratorSound s)
    {
        yield return new WaitForSeconds(soundDuration);
        if (s == lastNarrationSound) GameEventSystem.instance.cutsceneEvents.ClearSubtitles();
    }
}
