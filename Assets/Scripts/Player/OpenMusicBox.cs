using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMusicBox : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        gameObject.SetActive(false);
        transform.parent.Find("box-open").gameObject.SetActive(true);
        transform.parent.gameObject.GetComponent<AudioSource>().Stop();
        Destroy(gameObject.GetComponent<OpenMusicBox>());
    }
}
