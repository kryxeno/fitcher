using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMusicBox : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        gameObject.SetActive(false);
        transform.parent.Find("box-open").gameObject.SetActive(true);
        Destroy(gameObject.GetComponent<OpenMusicBox>());
    }
}
