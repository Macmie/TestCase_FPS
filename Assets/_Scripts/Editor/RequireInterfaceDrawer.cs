using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
public class RequireInterfaceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Check if this is reference type property.
        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            var requiredAttribute = this.attribute as RequireInterfaceAttribute;

            EditorGUI.BeginProperty(position, label, property);
            Object obj = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(Object), true);

            if (obj is GameObject g) property.objectReferenceValue = g.GetComponent(requiredAttribute.requiredType);
            EditorGUI.EndProperty();
        }
        else
        {
            var previousColor = GUI.color;
            GUI.color = Color.red;
            EditorGUI.LabelField(position, label, new GUIContent("Property is not a reference type"));
            GUI.color = previousColor;
        }
    }
}

