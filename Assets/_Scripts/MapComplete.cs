using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapComplete : MonoBehaviour
{
    [SerializeField] float _fadeSpeed;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
    }

    private IEnumerator Start()
    {
        while (_canvasGroup.alpha < .99f)
        {
            _canvasGroup.alpha += Time.deltaTime * _fadeSpeed/10;
            yield return null;
        }

        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield break;
    }
}
