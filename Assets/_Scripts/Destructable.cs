using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum Material {Concrete, Wood, Iron}

public class Destructable : MonoBehaviour
{
    public UnityEvent OnDestroy;
    [SerializeField] private Material _material;
    [SerializeField] private int _durability;

    public void TakeDamage(int dmg)
    {
        _durability -= dmg;
        if (_durability <= 0)
            ObjectDestroy();
    }

    private void ObjectDestroy()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }
}
