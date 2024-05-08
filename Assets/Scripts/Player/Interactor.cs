using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

interface IInteractible
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractorRange = 1f;
    public InteractionUI InteractionUI;
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
                    interactObj.Interact();
                    AudioManager.instance.Play("Click");
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
}
