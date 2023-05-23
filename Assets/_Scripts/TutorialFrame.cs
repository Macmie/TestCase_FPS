using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class TutorialFrame : MonoBehaviour
{
    [SerializeField] private TextMeshPro _tutorialText;
    [SerializeField] private TutorialFrame _nextFrame;
    [SerializeField] private float _fadeSpeed;

    public Action OnEntered;

    private void Awake()
    {
        OnEntered += SetNextFrame;
    }

    private void OnDestroy()
    {
        OnEntered -= SetNextFrame;
    }

    private void OnEnable()
    {
        FadeOut(false);
    }

    private void SetNextFrame()
    {
        if (_nextFrame)
            _nextFrame.gameObject.SetActive(true);
        FadeOut(true);
    }

    public void FadeOut(bool fade)
    {
        if (fade) StartCoroutine(Fade());
        else StartCoroutine(Show());
    }

    IEnumerator Fade()
    {
        while (_tutorialText.alpha > .01)
        {
            _tutorialText.alpha -= _fadeSpeed / 100;
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator Show()
    {
        while (_tutorialText.alpha < .99)
        {
            _tutorialText.alpha += _fadeSpeed / 100;
            yield return null;
        }
        _tutorialText.alpha = 1f;
    }
}
