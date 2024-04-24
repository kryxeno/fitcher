using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    private Camera _mainCam;

    private CanvasGroup _canvasGroup;

    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private float _fadeSpeed = 1f;
    private bool fadeIn = false;
    private bool fadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);

        if (fadeIn)
        {
            _canvasGroup.alpha += Time.deltaTime * _fadeSpeed;
            if (_canvasGroup.alpha >= 1)
            {
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            _canvasGroup.alpha -= Time.deltaTime * _fadeSpeed;
            if (_canvasGroup.alpha <= 0)
            {
                fadeOut = false;
            }
        }

    }

    public bool isDisplayed = false;

    public void SetUp(string promptText, Vector3 position)
    {
        _promptText.text = promptText;
        isDisplayed = true;
        fadeIn = true;
        fadeOut = false;
        transform.position = position;
    }

    public void Close()
    {
        isDisplayed = false;
        fadeOut = true;
        fadeIn = false;
    }

}
