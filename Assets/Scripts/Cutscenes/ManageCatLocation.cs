using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCatLocation : MonoBehaviour
{
    public Transform[] catLocations;

    private Vector3 initialScale;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    private void Start()
    {
        MoveCat(0);
    }

    private void OnEnable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart += CutsceneStart;
        GameEventSystem.instance.playerEvents.onCutsceneEnd += CutsceneEnd;
        GameEventSystem.instance.playerEvents.onMoveCat += MoveCat;
        GameEventSystem.instance.playerEvents.onShutUpCat += MuteCat;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.playerEvents.onCutsceneStart -= CutsceneStart;
        GameEventSystem.instance.playerEvents.onCutsceneEnd -= CutsceneEnd;
        GameEventSystem.instance.playerEvents.onMoveCat -= MoveCat;
        GameEventSystem.instance.playerEvents.onShutUpCat -= MuteCat;
    }

    private void MoveCat(int transformIndex)
    {
        for (int i = 0; i < catLocations.Length; i++)
        {
            if (i == transformIndex)
            {
                transform.position = catLocations[i].position;
                transform.eulerAngles = catLocations[i].eulerAngles;
                gameObject.GetComponent<AudioSource>().Play();
                break;
            }
        }
    }

    private void CutsceneStart()
    {
        transform.localScale = Vector3.zero;
        gameObject.GetComponent<AudioSource>().Stop();
    }

    private void CutsceneEnd(string cutsceneName)
    {
        transform.localScale = initialScale;
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void MuteCat()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
