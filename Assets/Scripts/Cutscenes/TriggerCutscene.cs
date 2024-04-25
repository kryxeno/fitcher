using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{

    public string cutsceneName;
    public bool lockPlayerMovement;
    public bool isSubTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CutsceneManager cutsceneManager = GameObject.Find("CutsceneManager").GetComponent<CutsceneManager>();
            cutsceneManager.PlayCutscene(cutsceneName, lockPlayerMovement);
            if (isSubTrigger) transform.parent.gameObject.SetActive(false);
            else gameObject.SetActive(false);
        }
    }
}
