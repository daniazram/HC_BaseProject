using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FieldToPropertyAttribute))]
public class FieldToPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attrib = attribute as FieldToPropertyAttribute;
        EditorGUI.PropertyField(position, property, new GUIContent(property.displayName));

        Debug.Log(attrib.propertyName);//property.serializedObject.FindProperty(attrib.propertyName).displayName);
    }
}