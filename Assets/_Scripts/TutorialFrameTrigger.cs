using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFrameTrigger : MonoBehaviour
{
    [SerializeField] private TutorialFrame _tutorialFrame;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _tutorialFrame.OnEntered?.Invoke();
    }
}
