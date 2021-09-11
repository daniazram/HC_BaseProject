using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class FieldToPropertyAttribute : PropertyAttribute
{
    public string propertyName;

    public FieldToPropertyAttribute(string propertyName)
    {
        this.propertyName = propertyName;
    }
}