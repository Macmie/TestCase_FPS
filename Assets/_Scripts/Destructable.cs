using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum ItemMaterial {Concrete, Wood, Iron}

public class Destructable : MonoBehaviour
{
    public UnityEvent OnDestroy;
    [SerializeField] private ItemMaterial _material;
    [SerializeField] private Renderer _objectRenderer;
    [SerializeField] private int _durability;
    [SerializeField] private Color _concreteColor, _ironColor, _woodColor;

    private void Awake()
    {
        SetMaterialColor();
    }
    public void TakeDamage(int dmg, ItemMaterial itemMaterial)
    {
        if (itemMaterial != _material) return;
        _durability -= dmg;
        if (_durability <= 0)
            ObjectDestroy();
    }

    public Color GetColor() => _objectRenderer.material.color;

    private void ObjectDestroy()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }

    private void SetMaterialColor() => _objectRenderer.material.color = GetColorFromMaterial();

    private Color GetColorFromMaterial()
    {
        switch (_material)
        {
            case ItemMaterial.Concrete: return _concreteColor;
            case ItemMaterial.Iron: return _ironColor;
            case ItemMaterial.Wood: return _woodColor;
            default:
                return Color.white;
        }
    }
}
