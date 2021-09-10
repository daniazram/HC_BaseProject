using UnityEngine;
using System.Linq;

public class DataBinderBase : MonoBehaviour
{
    [SerializeField]
    protected string targetProperty;
    [SerializeField]
    protected string sourceValue;

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

    protected virtual void Awake()
    {
        UnBind();
        Bind();
    }

    protected virtual void Start()
    { }

    protected virtual void OnDestroy()
    {
        UnBind();
    }

    public virtual void Bind()
    { }

    public virtual void UnBind()
    { }

    protected virtual BindedPropertyReference GetTargetPropertyReference(string propFullReference)
    {
        string typeName, memberName;
        GetTypeAndMemberName(propFullReference, out typeName, out memberName);

        return new BindedPropertyReference(GetComponent(typeName), memberName); ;
    }

    protected virtual BindedPropertyReference GetSourcePropertyReference(string propFullReference)
    {
        string typeName, memberName;
        GetTypeAndMemberName(propFullReference, out typeName, out memberName);

        var trans = transform;
        while (trans != null)
        {
            var components = trans.GetComponents<ViewModelBase>();
            var viewModel = components.Where(comp => comp.GetType().ToString() == typeName).FirstOrDefault();
            if(viewModel != null)
                return new BindedPropertyReference(viewModel, memberName);

            trans = trans.parent;
        }


        return null;
    }

    protected void GetTypeAndMemberName(string reference, out string typeName, out string memberName)
    {
        var lastPeriodIndex = reference.LastIndexOf('.');

        typeName = reference.Substring(0, lastPeriodIndex);
        memberName = reference.Substring(lastPeriodIndex + 1);

        if (typeName.StartsWith("UnityEngine."))
            typeName = typeName.Substring(typeName.LastIndexOf('.') + 1);
    }
}
