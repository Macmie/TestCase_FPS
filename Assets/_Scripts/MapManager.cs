using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField] Transform _enemiesRoot;

    public UnityEvent OnMissionComplete;

    private int _objectsToDestroy;

    private void Awake()
    {
        foreach (Transform child in _enemiesRoot)
        {
            if (child.TryGetComponent<Destructable>(out Destructable destructable)) //double check in case not-enemy object placed as child obj
            {
                destructable.OnDestroy.AddListener(OnObjectDestroyed);
                _objectsToDestroy++; 
            }
        }
    }

    public void OnObjectDestroyed()
    {
        _objectsToDestroy--;
        if (_objectsToDestroy <= 0)
            OnMissionComplete?.Invoke();
    }

    public void ReloadMap() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void ExitTheGame()
    {
#if UNITY_EDITOR
        Debug.Log("Game turned off. Have a nice evening");

#else
        Application.Quit();
#endif
    }
}
