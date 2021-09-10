using UnityEngine;

public class DataBinder : DataBinderBase
{
    private BindedPropertyReference targetReference;
    private BindedPropertyReference sourceValueReference;

    public override void Bind()
    {
        targetReference = GetTargetPropertyReference(TargetProperty);
        sourceValueReference = GetSourcePropertyReference(SourceValue);

        targetReference.BindTo(sourceValueReference, () => targetReference.SetValue(sourceValueReference.GetValue()));
        print($"Binded {targetReference.PropertyName} to {sourceValueReference.PropertyName}");

        //Sync the value on first run.
        targetReference.SetValue(sourceValueReference.GetValue());
    }

    public override void UnBind()
    {
        targetReference?.Dispose();
        sourceValueReference?.Dispose();
    }
}
