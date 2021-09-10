using System;

[AttributeUsage(AttributeTargets.Field)]
public class FieldToProperty : Attribute
{
    private string propertyName;

    public FieldToProperty(string propertyName)
    {
        this.propertyName = propertyName;
    }
}
