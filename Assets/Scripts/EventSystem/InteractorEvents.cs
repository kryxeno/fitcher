using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractorEvents
{
    public event Action onShowDiary;

    public void ShowDiary()
    {
        if (onShowDiary != null)
        {
            onShowDiary();
        }
    }

    public event Action onHideDiary;

    public void HideDiary()
    {
        if (onHideDiary != null)
        {
            onHideDiary();
        }
    }

    public event Action onShowNote;
    public void ShowNote()
    {
        if (onShowNote != null)
        {
            onShowNote();
        }
    }

    public event Action onHideNote;
    public void HideNote()
    {
        if (onHideNote != null)
        {
            onHideNote();
        }
    }

    public event Action onPickUpKey;
    public void PickUpKey()
    {
        if (onPickUpKey != null)
        {
            onPickUpKey();
        }
    }


    public event Action<int> onPageRead;
    public void PageRead(int pageNumber)
    {
        if (onPageRead != null)
        {
            onPageRead(pageNumber);
        }
    }

    public event Action onOpenCellarDoor;
    public void OpenCellarDoor()
    {
        if (onOpenCellarDoor != null)
        {
            onOpenCellarDoor();
        }
    }

    public event Action onSolveMorse;
    public void SolveMorse()
    {
        if (onSolveMorse != null)
        {
            onSolveMorse();
        }
    }
}
