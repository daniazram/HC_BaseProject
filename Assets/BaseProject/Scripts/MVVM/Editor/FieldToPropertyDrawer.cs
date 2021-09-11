using UnityEngine;
using UnityEditor;

using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(FieldToPropertyAttribute))]
public class FieldToPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attrib = attribute as FieldToPropertyAttribute;
        EditorGUI.PropertyField(position, property, new GUIContent(property.displayName));

        var owner = property.serializedObject.targetObject;
        var prop = GetProperty(owner, attrib.propertyName);
        prop.SetValue(owner, property.stringValue);
    }

    private PropertyInfo GetProperty(object owner, string propName)
    {
        return owner.GetType().GetProperty(propName, BindingFlags.Instance | BindingFlags.Public);
    }
}