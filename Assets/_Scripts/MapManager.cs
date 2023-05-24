using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] CanvasGroup _winnerCanvas;
    [SerializeField] Transform _enemiesRoot;

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
            GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
