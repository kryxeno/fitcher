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
        if (PlayerPrefs.GetInt("CellarDoorFound") == 1) AudioManager.instance.PlayNarration("RustyKey");
        Destroy(gameObject.GetComponent<OpenMusicBox>());
    }
}
