using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMusicBox : MonoBehaviour, IInteractible
{
    public AudioSource musicBoxAudio;
    public void Interact()
    {
        gameObject.SetActive(false);
        transform.parent.Find("box-open").gameObject.SetActive(true);
        musicBoxAudio.Stop();
        AudioManager.instance.PlayNarration("Musicbox");
        Destroy(gameObject.GetComponent<OpenMusicBox>());
    }
}
