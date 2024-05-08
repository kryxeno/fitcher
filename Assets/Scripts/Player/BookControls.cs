using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class BookControls : MonoBehaviour
{
    private bool isDiaryOpen = false;
    public bool hasFoundDiary = false;
    public Animator rightHandAnimator;
    public TwoBoneIKConstraint rightHandBookIK;
    public Material[] pageMaterials;
    public SkinnedMeshRenderer diaryPageObject;

    public CanvasGroup diaryUIButton;

    private CanvasGroup backUI;
    private CanvasGroup nextUI;

    private void Start()
    {
        backUI = GameObject.Find("PanelLeft").GetComponent<CanvasGroup>();
        nextUI = GameObject.Find("PanelRight").GetComponent<CanvasGroup>();
        ChangePage(0);
    }

    private void OnEnable()
    {
        GameEventSystem.instance.interactorEvents.onShowDiary += ShowDiary;
        GameEventSystem.instance.interactorEvents.onHideDiary += HideDiary;
    }

    private void OnDisable()
    {
        GameEventSystem.instance.interactorEvents.onShowDiary -= ShowDiary;
        GameEventSystem.instance.interactorEvents.onHideDiary -= HideDiary;
    }

    public void ShowDiary()
    {
        GameEventSystem.instance.interactorEvents.HideNote();
        gameObject.GetComponent<Animator>().SetBool("isActive", true);
        rightHandAnimator.SetBool("isGrabbing", true);
        rightHandBookIK.weight = 1f;
        isDiaryOpen = true;
        if (!hasFoundDiary)
        {
            hasFoundDiary = true;
            diaryUIButton.alpha = 1f;
        }
    }

    public void HideDiary()
    {
        gameObject.GetComponent<Animator>().SetBool("isActive", false);
        rightHandAnimator.SetBool("isGrabbing", false);
        isDiaryOpen = false;
    }

    private int currentPage = 0;

    public void ChangePage(int pageNumber)
    {
        currentPage = pageNumber;
        if (diaryPageObject != null)
        {
            Material[] materials = diaryPageObject.materials;
            materials[2] = pageMaterials[pageNumber];
            diaryPageObject.GetComponent<SkinnedMeshRenderer>().materials = materials;
        }
        else Debug.LogError("Diary Page Object not found");
        GameEventSystem.instance.interactorEvents.PageRead(pageNumber);
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isDiaryOpen) HideDiary();
        else if (Input.GetKeyDown(KeyCode.Alpha1) && !isDiaryOpen && hasFoundDiary) ShowDiary();

        if (Input.GetKeyDown(KeyCode.LeftArrow) && isDiaryOpen)
        {
            if (currentPage > 0)
            {
                ChangePage(currentPage - 1);
                AudioManager.instance.Play("PageLeft");
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && isDiaryOpen)
        {
            if (currentPage < pageMaterials.Length - 1)
            {
                ChangePage(currentPage + 1);
                AudioManager.instance.Play("PageRight");
            }
        }
    }

    private void UpdateUI()
    {
        if (currentPage == 0)
        {
            backUI.alpha = 0.2f;
            nextUI.alpha = 1f;
        }
        else if (currentPage == 2)
        {
            backUI.alpha = 1f;
            nextUI.alpha = 0.2f;
        }
        else
        {
            backUI.alpha = 1f;
            nextUI.alpha = 1f;
        }
    }

}
