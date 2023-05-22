using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFromMaterialChanger : MonoBehaviour
{
    [SerializeField, RequireInterface(typeof(IImpactMaterial))] private Object _objectWithMaterial;
    [SerializeField] private MaterialsColors _materialsColors;

    private IImpactMaterial _materialObject;

    private void Awake()
    {
        _materialObject = _objectWithMaterial as IImpactMaterial;
        ItemMaterial material = _materialObject.GetMaterial();
        Color color = _materialsColors.GetColorFromMaterial(material);
        _materialObject.SetRenderColorFromMaterial(color);
    }
}
