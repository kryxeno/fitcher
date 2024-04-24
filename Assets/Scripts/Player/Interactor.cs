using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<Note> notes = new List<Note>();
    public GameObject displayNote;

    private GameObject previousHitObject;

    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractorRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractible interactObj))
            {
                if (previousHitObject != null && previousHitObject != hitInfo.collider.gameObject) ResetOpacity(previousHitObject);

                InteractionUI.SetUp("E", hitInfo.collider.gameObject.transform.position);
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
        displayNote.GetComponent<Animator>().SetBool("isActive", true);
        Debug.Log("Note Name: " + note.noteName);
        Debug.Log("Note Text: " + note.noteText);
    }
}
