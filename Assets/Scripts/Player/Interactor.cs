using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

interface IInteractible
{
    public string Interact();
}

public class Note
{
    public string noteName;
    public string noteText;
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractorRange = 1f;
    public InteractionUI InteractionUI;

    public bool hasCellarKey = false;
    public bool hasFoundDiary = false;

    public List<Note> notes = new List<Note>();
    public GameObject displayNote;
    public GameObject displayDiary;
    public Animator rightHandAnimator;
    public TwoBoneIKConstraint rightHandBookIK;

    private GameObject previousHitObject;

    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractorRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractible interactObj))
            {
                if (previousHitObject != null && previousHitObject != hitInfo.collider.gameObject) ResetOpacity(previousHitObject);

                Transform UIInteractObject = hitInfo.collider.gameObject.transform.Find("UILocation");
                InteractionUI.SetUp("E", UIInteractObject ? UIInteractObject.position : hitInfo.collider.gameObject.transform.position);

                TurnOnOpacity(hitInfo.collider.gameObject);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    string itemTag = interactObj.Interact();

                    if (itemTag == "CellarKey") hasCellarKey = true;
                    if (itemTag == "Note")
                    {
                        PickUpNote note = hitInfo.collider.gameObject.GetComponent<PickUpNote>();
                        if (note != null)
                        {
                            Note newNote = new Note
                            {
                                noteName = note.noteName,
                                noteText = note.noteText
                            };
                            notes.Add(newNote);
                            ShowNote(newNote);
                        }
                    }
                    if (itemTag == "Diary")
                    {
                        ShowDiary();
                        hasFoundDiary = true;
                    }
                }

                previousHitObject = hitInfo.collider.gameObject;
            }
            else
            {
                if (previousHitObject != null)
                {
                    ResetOpacity(previousHitObject);
                    previousHitObject = null;
                }

                if (InteractionUI.isDisplayed) InteractionUI.Close();
            }
        }
        else
        {
            if (previousHitObject != null)
            {
                ResetOpacity(previousHitObject);
                previousHitObject = null;
            }

            if (InteractionUI.isDisplayed) InteractionUI.Close();
        }
    }

    void ResetOpacity(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_Opacity", 0f);
        renderer.SetPropertyBlock(propertyBlock);
    }

    void TurnOnOpacity(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_Opacity", 1f);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void ShowNote(Note note)
    {
        CloseDiary();
        displayNote.GetComponent<Animator>().SetBool("isActive", true);
        rightHandAnimator.SetBool("isGrabbing", true);
        rightHandBookIK.weight = 0f;
        Debug.Log("Note Name: " + note.noteName);
        Debug.Log("Note Text: " + note.noteText);
    }

    public void CloseNote()
    {
        displayNote.GetComponent<Animator>().SetBool("isActive", false);
        rightHandAnimator.SetBool("isGrabbing", false);
    }

    public void ShowDiary()
    {
        CloseNote();
        displayDiary.GetComponent<Animator>().SetBool("isActive", true);
        rightHandAnimator.SetBool("isGrabbing", true);
        rightHandBookIK.weight = 1f;
    }

    public void CloseDiary()
    {
        displayDiary.GetComponent<Animator>().SetBool("isActive", false);
        rightHandAnimator.SetBool("isGrabbing", false);
        rightHandBookIK.weight = 0f;
    }
}
