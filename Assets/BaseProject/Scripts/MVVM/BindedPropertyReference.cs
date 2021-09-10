using System;
using System.Reflection;
using System.ComponentModel;

public class BindedPropertyReference : IDisposable
{
    private bool disposed;
    private Action propertyChangedAction;
    private BindedPropertyReference propertyBindedTo;

    public object Owner { get; private set; }
    public PropertyInfo Property { get; private set; }
    public string PropertyName { get; private set; }

    public BindedPropertyReference(object owner, string propertyName)
    {
        Owner = owner;
        PropertyName = propertyName;
        Property = Owner.GetType().GetProperty(propertyName);
    }

    public object GetValue() => Property.GetValue(Owner);

    public void SetValue(object value) => Property.SetValue(Owner, value);

    public void BindTo(BindedPropertyReference propertyBindedTo, Action propertyChangedAction)
    {
        this.propertyBindedTo = propertyBindedTo;
        this.propertyChangedAction = propertyChangedAction;

        var viewModel = propertyBindedTo.Owner as INotifyPropertyChanged;
        viewModel.PropertyChanged += PropertyChangedHandler;
    }

    private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != propertyBindedTo.PropertyName)
            return;

        propertyChangedAction?.Invoke();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing && Owner != null)
        {
            var notifyPropertyChanged = Owner as INotifyPropertyChanged;
            if (notifyPropertyChanged != null)
                notifyPropertyChanged.PropertyChanged -= PropertyChangedHandler;

            Owner = null;
        }

        disposed = true;
    }
}