using UnityEngine;

public class DataBinder : DataBinderBase
{
    public override void Bind()
    {
        var targetReference = GetTargetPropertyReference(TargetProperty);
        var sourceValueReference = GetSourcePropertyReference(SourceValue);

        targetReference.BindTo(sourceValueReference, () => targetReference.SetValue(sourceValueReference.GetValue()));
        print($"Binded {targetReference.PropertyName} to {sourceValueReference.PropertyName}");

        //Sync the value on first run.
        //targetReference.SetValue(sourceValueReference.GetValue());
    }
}
