using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialColorsDatabase", menuName = "Database/MaterialsColors")]
public class MaterialsColors : ScriptableObject
{
    public Color ConcreteColor, WoodColor, IronColor;

    public Color GetColorFromMaterial(ItemMaterial material)
    {
        switch (material)
        {
            case ItemMaterial.Concrete: return ConcreteColor;
            case ItemMaterial.Iron: return IronColor;
            case ItemMaterial.Wood: return WoodColor;
            default:
                return Color.white;
        }
    }
}
