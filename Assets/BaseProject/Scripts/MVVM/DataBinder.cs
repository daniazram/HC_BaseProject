using UnityEngine;

public class DataBinder : MonoBehaviour
{
    [SerializeField]
    private string targetProperty;
    [SerializeField]
    private string sourceValue;

    public string TargetProperty
    {
        get => targetProperty;
        set => targetProperty = value;
    }

    public string SourceValue
    {
        get => sourceValue;
        set => sourceValue = value;
    }

    public virtual void Bind()
    {

    }
}
