using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IImpactMaterial 
{
    public ItemMaterial GetMaterial();

    public void SetRenderColorFromMaterial(Color color);
}
