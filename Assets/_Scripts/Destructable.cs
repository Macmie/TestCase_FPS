using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum ItemMaterial {Concrete, Wood, Iron}

public class Destructable : MonoBehaviour, IImpactMaterial
{
    public UnityEvent OnDestroy;
    [SerializeField] private ItemMaterial _material;
    [SerializeField] private Renderer _objectRenderer;
    [SerializeField] private int _durability;

    public void TakeDamage(int dmg, ItemMaterial itemMaterial)
    {
        if (itemMaterial != _material) return;
        _durability -= dmg;
        if (_durability <= 0)
            ObjectDestroy();
    }

    public Color GetColor() => _objectRenderer.material.color;

    public ItemMaterial GetMaterial() => _material;

    public void SetRenderColorFromMaterial(Color color) => _objectRenderer.material.color = color;

    private void ObjectDestroy()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }
}
