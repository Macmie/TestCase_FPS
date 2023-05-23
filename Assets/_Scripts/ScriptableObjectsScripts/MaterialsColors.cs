using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialColorsDatabase", menuName = "Database/MaterialsColors")]
public class MaterialsColors : ScriptableObject
{
    [SerializeField] private Color _concreteColor, _woodColor, _ironColor;

    public Color GetColorFromMaterial(ItemMaterial material)
    {
        switch (material)
        {
            case ItemMaterial.Concrete: 
                return _concreteColor;
            case ItemMaterial.Iron: 
                return _ironColor;
            case ItemMaterial.Wood: 
                return _woodColor;
            default:
                return Color.white;
        }
    }
}
