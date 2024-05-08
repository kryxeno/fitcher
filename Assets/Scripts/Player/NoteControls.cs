using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NoteControls : MonoBehaviour
{
    public bool isNoteOpen = false;
    public bool hasFoundNote = false;
    public Animator rightHandAnimator;
    public TwoBoneIKConstraint rightHandBookIK;

    public CanvasGroup noteUIButton;


    private void OnEnable()
    {
        GameEventSystem.instance.interactorEvents.onShowNote += ShowNote;
        GameEventSystem.instance.interactorEvents.onHideNote += HideNote;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.interactorEvents.onShowNote -= ShowNote;
        GameEventSystem.instance.interactorEvents.onHideNote -= HideNote;
    }

    public void ShowNote()
    {
        GameEventSystem.instance.interactorEvents.HideDiary();
        gameObject.GetComponent<Animator>().SetBool("isActive", true);
        rightHandAnimator.SetBool("isGrabbing", true);
        rightHandBookIK.weight = 0f;
        isNoteOpen = true;
        if (!hasFoundNote)
        {
            hasFoundNote = true;
            noteUIButton.alpha = 1f;
        }
    }

    public void HideNote()
    {
        gameObject.GetComponent<Animator>().SetBool("isActive", false);
        rightHandAnimator.SetBool("isGrabbing", false);
        isNoteOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && isNoteOpen && hasFoundNote)
        {
            HideNote();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !isNoteOpen && hasFoundNote)
        {
            ShowNote();
        }
    }
}
