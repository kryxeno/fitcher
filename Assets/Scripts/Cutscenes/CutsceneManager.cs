using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public List<PlayableDirector> cutscenes;
    public FirstPersonController playerController;
    public TextMeshProUGUI skipText;
    public float skipTimer = 3f;

    private PlayableDirector currentCutscene;
    private float timer = 0f;

    void Start()
    {
        GameObject cutsceneParent = GameObject.Find("Cutscenes");
        foreach (Transform child in cutsceneParent.transform)
        {
            if (child.GetComponent<PlayableDirector>() != null)
            {
                cutscenes.Add(child.GetComponent<PlayableDirector>());
            }
        }
        PlayCutscene("IntroCutscene", true);
    }

    public void PlayCutscene(string cutsceneName, bool lockPlayerMovement)
    {
        foreach (PlayableDirector cutscene in cutscenes)
        {
            if (cutscene.name == cutsceneName)
            {
                Debug.Log("Playing Cutscene: " + cutsceneName);
                if (lockPlayerMovement) GameEventSystem.instance.playerEvents.CutsceneStart();
                cutscene.Play();
                cutscene.stopped += OnCutsceneFinished;
                currentCutscene = cutscene;
            }
        }
    }

    void OnCutsceneFinished(PlayableDirector aDirector)
    {
        GameEventSystem.instance.playerEvents.CutsceneEnd();
        aDirector.stopped -= OnCutsceneFinished;
        currentCutscene = null;
        AudioManager.instance.Play("Ambiance");
    }

    private bool canSkipCutscene = false;

    void Update()
    {
        if (canSkipCutscene && currentCutscene != null)
        {
            timer += Time.deltaTime;
            if (timer >= skipTimer)
            {
                canSkipCutscene = false;
                skipText.gameObject.SetActive(false);
                timer = 0f;
            }
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && currentCutscene != null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                skipText.transform.Find("Enter").gameObject.SetActive(true);
                skipText.transform.Find("Space").gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                skipText.transform.Find("Enter").gameObject.SetActive(false);
                skipText.transform.Find("Space").gameObject.SetActive(true);
            }

            if (canSkipCutscene)
            {
                currentCutscene.Stop();
            }
            else if (!canSkipCutscene)
            {
                canSkipCutscene = true;
                skipText.gameObject.SetActive(true);
            }
        }
    }

}
